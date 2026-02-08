using Microsoft.EntityFrameworkCore;
using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _db;

        public ExpenseService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Expense>> GetUserExpensesAsync(int userId)
        {
            return await _db.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task<List<Expense>> GetRecentExpensesAsync(int userId)
        {
            return await _db.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .Take(5)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalExpensesForMonthAsync(int userId, DateTime month)
        {
            // Fetch to list first to avoid SQLite decimal sum issues
            var expenses = await _db.Expenses
                .Where(e => e.UserId == userId && e.Date.Year == month.Year && e.Date.Month == month.Month)
                .ToListAsync();

            return expenses.Sum(e => e.Amount);
        }

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            try
            {
                _db.Expenses.Add(expense);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteExpenseAsync(int expenseId, int userId)
        {
            var expense = await _db.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense == null) return false;

            _db.Expenses.Remove(expense);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
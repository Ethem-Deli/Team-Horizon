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

        // Implementation for GetRecentExpensesAsync(int userId)
        public async Task<List<Expense>> GetRecentExpensesAsync(int userId)
        {
            return await _db.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .Take(5) // Default to 5 most recent
                .ToListAsync();
        }

        // Implementation for GetTotalExpensesForMonthAsync(int userId, DateTime date)
        public async Task<decimal> GetTotalExpensesForMonthAsync(int userId, DateTime date)
        {
            var expenses = await _db.Expenses
                .Where(e => e.UserId == userId &&
                            e.Date.Year == date.Year &&
                            e.Date.Month == date.Month)
                .ToListAsync();

            return expenses.Sum(e => e.Amount);
        }

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            _db.Expenses.Add(expense);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateExpenseAsync(Expense expense, int userId)
        {
            var existing = await _db.Expenses
                .FirstOrDefaultAsync(e => e.Id == expense.Id && e.UserId == userId);

            if (existing == null) return false;

            existing.Amount = expense.Amount;
            existing.Description = expense.Description;
            existing.Date = expense.Date;
            existing.CategoryId = expense.CategoryId;

            _db.Expenses.Update(existing);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteExpenseAsync(int expenseId, int userId)
        {
            var expense = await _db.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense == null) return false;

            _db.Expenses.Remove(expense);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
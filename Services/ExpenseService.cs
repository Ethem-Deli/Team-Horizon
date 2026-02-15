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
            // SECURITY: If user is not logged in, return empty list (no data leakage).
            if (userId <= 0) return new List<Expense>();

            // SECURITY: Always filter by UserId so users only see their own expenses.
            return await _db.Expenses
                .AsNoTracking()
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task<List<Expense>> GetRecentExpensesAsync(int userId)
        {
            // SECURITY: If user is not logged in, return empty list (no data leakage).
            if (userId <= 0) return new List<Expense>();

            // SECURITY: Always filter by UserId so users only see their own expenses.
            return await _db.Expenses
                .AsNoTracking()
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .Take(5)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalExpensesForMonthAsync(int userId, DateTime date)
        {
            // SECURITY: If user is not logged in, return 0.
            if (userId <= 0) return 0m;

            // SQLite can't SUM() decimals reliably via EF translation.
            // Sum as double in SQL, then convert back to decimal.
            var total = await _db.Expenses
                .AsNoTracking()
                .Where(e => e.UserId == userId && e.Date.Year == date.Year && e.Date.Month == date.Month)
                .SumAsync(e => (double?)e.Amount) ?? 0d;

            return (decimal)total;
        }

        public async Task<List<Expense>> GetExpensesForMonthAsync(int userId, DateTime monthStart)
        {
            var monthEnd = monthStart.AddMonths(1);

            return await _db.Expenses
                .Where(e => e.UserId == userId &&
                            e.Date >= monthStart &&
                            e.Date < monthEnd)
                .ToListAsync();
        }

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            // SECURITY: Do not allow inserting expenses without a valid user.
            if (expense == null || expense.UserId <= 0) return false;

            // SECURITY: Expense belongs to the current user (UserId must be set by the app).
            _db.Expenses.Add(expense);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateExpenseAsync(Expense expense, int userId)
        {
            // SECURITY: Only allow updates for a valid logged-in user.
            if (expense == null || userId <= 0) return false;

            // SECURITY: Update only if the expense belongs to this user (Id + UserId check).
            var existing = await _db.Expenses
                .FirstOrDefaultAsync(e => e.Id == expense.Id && e.UserId == userId);

            if (existing == null) return false;

            // Update only allowed fields (ownership stays unchanged).
            existing.Amount = expense.Amount;
            existing.Description = expense.Description;
            existing.Date = expense.Date;
            existing.CategoryId = expense.CategoryId;

            _db.Expenses.Update(existing);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteExpenseAsync(int expenseId, int userId)
        {
            // SECURITY: Only allow delete for a valid logged-in user.
            if (userId <= 0 || expenseId <= 0) return false;

            // SECURITY: Delete only if expense belongs to this user (Id + UserId check).
            var expense = await _db.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense == null) return false;

            _db.Expenses.Remove(expense);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}

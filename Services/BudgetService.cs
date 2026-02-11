using Microsoft.EntityFrameworkCore;
using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _db;

        public BudgetService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Budget?> GetBudgetForMonthAsync(int userId, int year, int month)
        {
            // SECURITY: If user is not logged in, return no data.
            if (userId <= 0) return null;

            // SECURITY: Always filter by UserId to enforce user isolation.
            return await _db.Budgets
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Year == year && b.Month == month);
        }

        public async Task<List<Budget>> GetUserBudgetsAsync(int userId)
        {
            // SECURITY: If user is not logged in, return empty list (no data leakage).
            if (userId <= 0) return new List<Budget>();

            // SECURITY: Always filter by UserId to enforce user isolation.
            return await _db.Budgets
                .AsNoTracking()
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.Year)
                .ThenByDescending(b => b.Month)
                .ToListAsync();
        }

        public async Task<bool> UpsertBudgetAsync(Budget budget)
        {
            // SECURITY: Do not allow inserting/updating budgets without a valid user.
            if (budget == null || budget.UserId <= 0) return false;

            // SECURITY: Look up existing budget for THIS user only (no cross-user overwrite).
            var existing = await _db.Budgets
                .FirstOrDefaultAsync(b => b.UserId == budget.UserId && b.Year == budget.Year && b.Month == budget.Month);

            if (existing != null)
            {
                // Update only allowed fields (keep ownership intact).
                existing.Amount = budget.Amount;
                _db.Budgets.Update(existing);
            }
            else
            {
                // New budget belongs to the current user.
                _db.Budgets.Add(budget);
            }

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBudgetAsync(int budgetId, int userId)
        {
            // SECURITY: Only allow delete for a valid logged-in user.
            if (userId <= 0 || budgetId <= 0) return false;

            // SECURITY: Delete only if budget belongs to this user (Id + UserId check).
            var budget = await _db.Budgets
                .FirstOrDefaultAsync(b => b.Id == budgetId && b.UserId == userId);

            if (budget == null) return false;

            _db.Budgets.Remove(budget);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}

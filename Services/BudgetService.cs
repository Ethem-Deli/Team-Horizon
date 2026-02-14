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

        // Implementation for GetBudgetForMonthAsync
        public async Task<Budget?> GetBudgetForMonthAsync(int userId, int year, int month)
        {
            return await _db.Budgets
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Year == year && b.Month == month);
        }

        public async Task<List<Budget>> GetUserBudgetsAsync(int userId)
        {
            return await _db.Budgets
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.Year)
                .ThenByDescending(b => b.Month)
                .ToListAsync();
        }

        public async Task<bool> UpsertBudgetAsync(Budget budget)
        {
            var existing = await GetBudgetForMonthAsync(budget.UserId, budget.Year, budget.Month);

            if (existing != null)
            {
                existing.Amount = budget.Amount;
                _db.Budgets.Update(existing);
            }
            else
            {
                _db.Budgets.Add(budget);
            }

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<Budget?> GetBudgetAsync(int userId, int year, int month)
        {
            return await _db.Budgets
                .FirstOrDefaultAsync(b =>
                    b.UserId == userId &&
                    b.Year == year &&
                    b.Month == month);
        }

        public async Task<bool> DeleteBudgetAsync(int budgetId, int userId)
        {
            var budget = await _db.Budgets
                .FirstOrDefaultAsync(b => b.Id == budgetId && b.UserId == userId);

            if (budget == null) return false;

            _db.Budgets.Remove(budget);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
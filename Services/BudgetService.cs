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

        public async Task<Budget?> GetBudgetAsync(int userId, int year, int month)
        {
            return await _db.Budgets
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Year == year && b.Month == month);
        }

        public async Task<Budget?> GetBudgetForMonthAsync(int userId, int month, int year)
        {
            return await _db.Budgets
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Month == month && b.Year == year);
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
            var existing = await GetBudgetAsync(budget.UserId, budget.Year, budget.Month);

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
    }
}
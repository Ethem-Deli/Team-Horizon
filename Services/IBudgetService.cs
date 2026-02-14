using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public interface IBudgetService
    {
        Task<Budget?> GetBudgetForMonthAsync(int userId, int year, int month);
        Task<Budget?> GetBudgetAsync(int userId, int year, int month);
        Task<List<Budget>> GetUserBudgetsAsync(int userId);
        Task<bool> UpsertBudgetAsync(Budget budget);
        Task<bool> DeleteBudgetAsync(int budgetId, int userId);
    }
}
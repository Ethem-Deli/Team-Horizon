using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public interface IBudgetService
    {
        Task<Budget?> GetBudgetAsync(int userId, int year, int month);
        Task<Budget?> GetBudgetForMonthAsync(int userId, int month, int year);
        Task<List<Budget>> GetUserBudgetsAsync(int userId);

        Task<bool> UpsertBudgetAsync(Budget budget);
    }
}
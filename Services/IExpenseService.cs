using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetUserExpensesAsync(int userId);
        Task<decimal> GetTotalExpensesForMonthAsync(int userId, DateTime month);
        Task<List<Expense>> GetRecentExpensesAsync(int userId);
        Task<bool> AddExpenseAsync(Expense expense);
        Task<bool> DeleteExpenseAsync(int expenseId, int userId);
    }
}
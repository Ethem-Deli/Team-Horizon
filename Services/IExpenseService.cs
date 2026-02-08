using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetUserExpensesAsync(int userId);
        Task<List<Expense>> GetRecentExpensesAsync(int userId);
        Task<decimal> GetTotalExpensesForMonthAsync(int userId, DateTime date);
        Task<bool> AddExpenseAsync(Expense expense);
        Task<bool> UpdateExpenseAsync(Expense expense, int userId);
        Task<bool> DeleteExpenseAsync(int expenseId, int userId);
    }
}
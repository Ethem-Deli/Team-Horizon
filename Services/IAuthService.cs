using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(string username, string password);

        /// <summary>
        /// Checks if a username or email is already taken.
        /// </summary>
        Task<bool> UserExistsAsync(string username, string email);

        void Logout();
    }
}
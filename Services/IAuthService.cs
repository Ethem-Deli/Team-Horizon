using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Validates credentials and updates UserState if successful.
        /// </summary>
        /// <returns>The authenticated user or null if failed.</returns>
        Task<User?> LoginAsync(string username, string password);

        /// <summary>
        /// Clears the current session.
        /// </summary>
        void Logout();
    }
}
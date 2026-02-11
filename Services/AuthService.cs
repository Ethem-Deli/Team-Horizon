using Microsoft.EntityFrameworkCore;
using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Models;
using FamilyBudgetExpenseTracker.Security;

namespace FamilyBudgetExpenseTracker.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserState _userState;

        public AuthService(ApplicationDbContext db, UserState userState)
        {
            _db = db;
            _userState = userState;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            // Fetch user by username (case-insensitive)
            var user = await _db.Users.FirstOrDefaultAsync(u =>
                u.Username.ToLower() == username.ToLower());

            // Verify password hash
            if (user != null && PasswordHasher.Verify(password, user.PasswordHash))
            {
                // ✅ Persist session login (refresh will stay logged in)
                await _userState.LoginAsync(user);
                return user;
            }

            return null;
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _db.Users.AnyAsync(u =>
                u.Username.ToLower() == username.ToLower() ||
                u.Email.ToLower() == email.ToLower());
        }

        // ✅ Interface requires Logout() (sync). Keep it and call async internally.
        public void Logout()
        {
            _userState.Logout(); // uses compatibility wrapper -> LogoutAsync()
        }
    }
}

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

            // 1. Fetch user by username (case-insensitive)
            var user = await _db.Users.FirstOrDefaultAsync(u =>
                u.Username.ToLower() == username.ToLower());

            // 2. Verify password hash
            if (user != null && PasswordHasher.Verify(password, user.PasswordHash))
            {
                // 3. Update the global session state
                _userState.Login(user);
                return user;
            }

            return null;
        }

        public void Logout()
        {
            _userState.Logout();
        }
    }
}
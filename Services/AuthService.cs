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

            // We fetch the user first
            var user = await _db.Users.FirstOrDefaultAsync(u =>
                u.Username.ToLower() == username.ToLower());

            // We verify. If either fails, we return null. 
            // The UI will handle this by showing a generic "Invalid credentials" message.
            if (user != null && PasswordHasher.Verify(password, user.PasswordHash))
            {
                _userState.Login(user);
                return user;
            }

            return null;
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            // Check both username and email for duplicates
            return await _db.Users.AnyAsync(u =>
                u.Username.ToLower() == username.ToLower() ||
                u.Email.ToLower() == email.ToLower());
        }

        public void Logout()
        {
            _userState.Logout();
        }
    }
}
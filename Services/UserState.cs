using System;
using System.Threading.Tasks;
using FamilyBudgetExpenseTracker.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace FamilyBudgetExpenseTracker.Services
{
    public class UserState
    {
        private readonly ProtectedSessionStorage _session;
        private bool _restored;

        public UserState(ProtectedSessionStorage session)
        {
            _session = session;
        }

        public User? CurrentUser { get; private set; }
        public bool IsAuthenticated => CurrentUser != null;
        public int CurrentUserId => CurrentUser?.Id ?? 0;
        public string CurrentUserName => CurrentUser?.FullName ?? "Guest";

        public event Action? OnChange;

        // Restore login after browser refresh (session storage)
        public async Task RestoreAsync()
        {
            if (_restored) return;
            _restored = true;

            try
            {
                var result = await _session.GetAsync<User>("current_user");
                if (result.Success && result.Value != null)
                {
                    CurrentUser = result.Value;
                    NotifyStateChanged();
                }
            }
            catch
            {
                // ignore for assignment
            }
        }

        //  NEW: Async login (recommended)
        public async Task LoginAsync(User user)
        {
            CurrentUser = user;
            await _session.SetAsync("current_user", user);
            NotifyStateChanged();
        }

        //  NEW: Async logout (recommended)
        public async Task LogoutAsync()
        {
            CurrentUser = null;
            await _session.DeleteAsync("current_user");
            NotifyStateChanged();
        }

        //  COMPATIBILITY: old sync method still used by some files
        public void Login(User user)
        {
            // Fire-and-forget (good enough for assignment)
            _ = LoginAsync(user);
        }

        //  COMPATIBILITY: old sync method still used by some files
        public void Logout()
        {
            _ = LogoutAsync();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

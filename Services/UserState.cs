using System;
using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public class UserState
    {
        // The core session data
        public User? CurrentUser { get; private set; } = null;
        public bool IsAuthenticated => CurrentUser != null;
        public int CurrentUserId => CurrentUser?.Id ?? 0;
        public string CurrentUserName => CurrentUser?.FullName ?? "Guest";

        // Event for UI components to listen for (e.g., NavMenu)
        public event Action? OnChange;

        public void Login(User user)
        {
            CurrentUser = user;
            NotifyStateChanged();
        }

        public void Logout()
        {
            CurrentUser = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace FamilyBudgetExpenseTracker.Services
{
    public class AppAuthStateProvider : AuthenticationStateProvider
    {
        private readonly UserState _userState;

        public AppAuthStateProvider(UserState userState)
        {
            _userState = userState;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // If CurrentUserId is 0 (or default), treat as not logged in
            if (_userState.CurrentUserId <= 0)
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return Task.FromResult(new AuthenticationState(anonymous));
            }

            // Logged in user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, _userState.CurrentUserId.ToString()),
                new Claim(ClaimTypes.Name, _userState.CurrentUserId.ToString())
            };

            var identity = new ClaimsIdentity(claims, authenticationType: "AppAuth");
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        // Call this when UserState changes (after login/logout) to refresh UI authorization
        public void NotifyUserStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}

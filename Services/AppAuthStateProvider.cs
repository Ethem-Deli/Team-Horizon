using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace FamilyBudgetExpenseTracker.Services
{
    public class AppAuthStateProvider : AuthenticationStateProvider
    {
        private readonly UserState _userState;

        public AppAuthStateProvider(UserState userState)
        {
            _userState = userState;

            // When UserState changes (login/logout), notify Blazor auth system
            _userState.OnChange += NotifyAuthChanged;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // If not logged in => anonymous user
            if (!_userState.IsAuthenticated || _userState.CurrentUser == null)
            {
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return Task.FromResult(new AuthenticationState(anonymous));
            }

            // Logged in => create an authenticated ClaimsPrincipal
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, _userState.CurrentUser.Id.ToString()),
                new Claim(ClaimTypes.Name, _userState.CurrentUser.FullName ?? "User"),
                new Claim(ClaimTypes.Email, _userState.CurrentUser.Email ?? "")
            };

            var identity = new ClaimsIdentity(claims, authenticationType: "AppAuth");
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        private void NotifyAuthChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}

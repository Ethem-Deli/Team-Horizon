using FamilyBudgetExpenseTracker.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudgetExpenseTracker.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // Optional helper if you want to keep Program.cs clean.
        public static IServiceCollection AddFamilyBudgetServices(this IServiceCollection services)
        {
            services.AddScoped<ProtectedSessionStorage>();
            services.AddScoped<UserState>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IBudgetService, BudgetService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddAuthorizationCore();
            services.AddCascadingAuthenticationState();
            services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider>();

            services.AddScoped<MonthlyReportService>();
            services.AddScoped<IReportService, ReportService>();

            return services;
        }
    }
}

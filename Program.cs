using System.IO;
using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages + Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    // Show detailed circuit errors only in Development
    options.DetailedErrors = builder.Environment.IsDevelopment();
});

// Ensure temp folder exists (Render Free supports /tmp)
Directory.CreateDirectory("/tmp");

// Database (Render Free: store SQLite in /tmp)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=/tmp/familybudget.db"));

// ✅ REQUIRED: Enables session storage used by UserState (prevents logout on refresh)
builder.Services.AddScoped<ProtectedSessionStorage>();

// App State & Reports
builder.Services.AddScoped<UserState>();
builder.Services.AddScoped<MonthlyReportService>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// ✅ REQUIRED for [Authorize] to work in Blazor components
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider>();

var app = builder.Build();

// Bind to Render’s PORT if provided
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrWhiteSpace(port))
{
    app.Urls.Add($"http://0.0.0.0:{port}");
}

if (!app.Environment.IsDevelopment())
{
    // Friendly error page in Production
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    // Developer exception page in Dev
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Send 404/403/etc to /Error?code=xxx
app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Create DB if not exists
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
}

app.Run();

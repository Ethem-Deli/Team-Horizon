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
    options.DetailedErrors = builder.Environment.IsDevelopment();
});

// Pick DB path:
// - Local dev: store in project folder (works on Windows/macOS/Linux)
// - Production (Render free): store in /tmp (Linux)
string dbPath;

if (builder.Environment.IsDevelopment())
{
    // Save DB in your app folder locally
    dbPath = Path.Combine(builder.Environment.ContentRootPath, "familybudget.dev.db");
}
else
{
    // Render free: /tmp is writable
    Directory.CreateDirectory("/tmp");
    dbPath = "/tmp/familybudget.db";
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

//REQUIRED: Enables session storage used by UserState
builder.Services.AddScoped<ProtectedSessionStorage>();

// App State & Reports
builder.Services.AddScoped<UserState>();
builder.Services.AddScoped<MonthlyReportService>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider>();

var app = builder.Build();

// Bind to Render PORT if provided
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrWhiteSpace(port))
{
    app.Urls.Add($"http://0.0.0.0:{port}");
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

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

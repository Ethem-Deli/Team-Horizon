using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages + Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    // Show detailed circuit errors only in Development
    options.DetailedErrors = builder.Environment.IsDevelopment();
});

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=familybudget.db"));

// App State & Reports
builder.Services.AddScoped<UserState>();
builder.Services.AddScoped<MonthlyReportService>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Auth for Blazor UI (your existing setup)
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

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

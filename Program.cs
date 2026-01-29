using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=familybudget.db"));

// Session state
builder.Services.AddScoped<UserState>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
    SeedData.Initialize(db);
}
app.Run();
using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Data;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Check if data already exists
        if (context.Users.Any())
        {
            return;
        }

        // Create demo user
        var demoUser = new User
        {
            Email = "demo@example.com",
            PasswordHash = "demo123", // In real app, hash this!
            FullName = "Demo User"
        };
        context.Users.Add(demoUser);
        context.SaveChanges();

        // Create demo categories
        var categories = new List<Category>
        {
            new Category { Name = "Groceries", Icon = "🛒", ColorCode = "#27ae60", UserId = demoUser.Id },
            new Category { Name = "Transportation", Icon = "🚗", ColorCode = "#3498db", UserId = demoUser.Id },
            new Category { Name = "Utilities", Icon = "💡", ColorCode = "#e74c3c", UserId = demoUser.Id },
            new Category { Name = "Entertainment", Icon = "🎬", ColorCode = "#9b59b6", UserId = demoUser.Id },
            new Category { Name = "Healthcare", Icon = "⚕️", ColorCode = "#e67e22", UserId = demoUser.Id }
        };
        context.Categories.AddRange(categories);
        context.SaveChanges();

        // Create demo expenses
        var expenses = new List<Expense>
        {
            new Expense { Amount = 125.50m, Date = DateTime.Today.AddDays(-5), Description = "Weekly groceries", UserId = demoUser.Id, CategoryId = categories[0].Id },
            new Expense { Amount = 45.00m, Date = DateTime.Today.AddDays(-3), Description = "Gas station", UserId = demoUser.Id, CategoryId = categories[1].Id },
            new Expense { Amount = 89.99m, Date = DateTime.Today.AddDays(-2), Description = "Electric bill", UserId = demoUser.Id, CategoryId = categories[2].Id },
            new Expense { Amount = 35.00m, Date = DateTime.Today.AddDays(-1), Description = "Movie tickets", UserId = demoUser.Id, CategoryId = categories[3].Id }
        };
        context.Expenses.AddRange(expenses);
        context.SaveChanges();

        // Create demo budget
        var budget = new Budget
        {
            Month = DateTime.Today.Month,
            Year = DateTime.Today.Year,
            Amount = 2000.00m,
            UserId = demoUser.Id
        };
        context.Budgets.Add(budget);
        context.SaveChanges();
    }
}
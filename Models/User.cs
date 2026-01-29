namespace FamilyBudgetExpenseTracker.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string FullName { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public List<Expense> Expenses { get; set; } = new();
    public List<Budget> Budgets { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}
namespace FamilyBudgetTracker.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
    
    [Required]
    public string PasswordHash { get; set; } = "";
    
    [Required]
    public string FullName { get; set; } = "";
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    // Navigation properties
    public List<Expense> Expenses { get; set; } = new();
    public List<Budget> Budgets { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}
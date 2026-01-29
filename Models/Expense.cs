namespace FamilyBudgetExpenseTracker.Models;

public class Expense
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public string Description { get; set; } = "";

    // Foreign keys
    public int UserId { get; set; }
    public int CategoryId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Category Category { get; set; } = null!;
}
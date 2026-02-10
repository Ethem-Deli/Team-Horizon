namespace FamilyBudgetExpenseTracker.Models;


public class Budget
{
    public int Id { get; set; }
    public int Month { get; set; }  // 1-12
    public int Year { get; set; }
    public decimal Amount { get; set; }

    // Foreign keys
    public int UserId { get; set; }
    public int? CategoryId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Category? Category { get; set; }
}
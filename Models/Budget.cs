namespace FamilyBudgetTracker.Models;

public class Budget
{
    public int Id { get; set; }
    
    [Required]
    public int Month { get; set; }  // 1-12
    
    [Required]
    public int Year { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
    
    // Foreign keys
    public int UserId { get; set; }
    public int? CategoryId { get; set; }  // Null = overall budget
    
    // Navigation properties
    public User User { get; set; } = null!;
    public Category? Category { get; set; }
}
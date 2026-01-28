namespace FamilyBudgetTracker.Models;

public class Expense
{
    public int Id { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }
    
    [Required]
    public DateTime Date { get; set; } = DateTime.Today;
    
    [Required]
    [StringLength(200)]
    public string Description { get; set; } = "";
    
    // Foreign keys
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public Category Category { get; set; } = null!;
}
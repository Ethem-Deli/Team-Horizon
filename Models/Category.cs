namespace FamilyBudgetTracker.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = "";
    
    public string? Description { get; set; }
    
    public string ColorCode { get; set; } = "#000000";
    
    public string Icon { get; set; } = "📁";
    
    // Foreign key
    public int UserId { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public List<Expense> Expenses { get; set; } = new();
}
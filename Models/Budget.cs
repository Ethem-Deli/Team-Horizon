using System.ComponentModel.DataAnnotations;

namespace FamilyBudgetExpenseTracker.Models;

public class Budget
{
    public int Id { get; set; }

    [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
    public int Month { get; set; }  // 1-12

    [Range(2000, 2100, ErrorMessage = "Year must be between 2000 and 2100.")]
    public int Year { get; set; }

    [Range(typeof(decimal), "0.01", "100000000", ErrorMessage = "Budget amount must be greater than 0.")]
    public decimal Amount { get; set; }

    // Foreign keys
    public int UserId { get; set; }
    public int? CategoryId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Category? Category { get; set; }
}
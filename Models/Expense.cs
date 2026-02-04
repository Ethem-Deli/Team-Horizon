using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyBudgetExpenseTracker.Models;

public class Expense
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.Today;

    [StringLength(250)]
    public string Description { get; set; } = string.Empty;

    // ---------- Foreign Keys ----------
    [Required]
    public int UserId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    // ---------- Navigation Properties ----------
    public User? User { get; set; }

    public Category? Category { get; set; }
}
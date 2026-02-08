using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamilyBudgetExpenseTracker.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }

        [StringLength(7)]
        public string ColorCode { get; set; } = "#3498db";

        [StringLength(10)] // Increased slightly for complex emojis
        public string Icon { get; set; } = "📁";

        // Foreign key to User
        [Required]
        public int UserId { get; set; }

        // Navigation properties
        public User? User { get; set; }

        // One Category can have many Expenses
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
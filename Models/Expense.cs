using System;
using System.ComponentModel.DataAnnotations;


namespace FamilyBudgetExpenseTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        [Range(0.01, 10000000)]
        public decimal Amount { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.Now;

        // Foreign Key to Category
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Foreign Key to User
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
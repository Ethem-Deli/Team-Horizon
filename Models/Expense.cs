using System;
using System.ComponentModel.DataAnnotations;
using FamilyBudgetExpenseTracker.Utilities.Validation;


namespace FamilyBudgetExpenseTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        [Range(0.01, 10000000)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 200 characters.")]
        [RegularExpression(@".*\S.*", ErrorMessage = "Description cannot be empty.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [NotInFutureDate(ErrorMessage = "Date cannot be in the future.")]
        public DateTime Date { get; set; } = DateTime.Now;

        // Foreign Key to Category
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Foreign Key to User
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
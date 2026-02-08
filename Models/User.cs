using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamilyBudgetExpenseTracker.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public List<Expense> Expenses { get; set; } = new();
        public List<Budget> Budgets { get; set; } = new();

        // FIX: Added the missing Categories collection
        public List<Category> Categories { get; set; } = new();
    }
}
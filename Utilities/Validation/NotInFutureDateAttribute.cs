using System;
using System.ComponentModel.DataAnnotations;

namespace FamilyBudgetExpenseTracker.Utilities.Validation
{
    /// <summary>
    /// Validates that a DateTime value is not in the future (compared to local server date).
    /// Useful for expenses, where future-dated entries are typically invalid.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class NotInFutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null) return true; // [Required] should handle nulls

            if (value is DateTime dt)
            {
                // Compare by date only (ignore time component)
                return dt.Date <= DateTime.Today;
            }

            return false;
        }
    }
}

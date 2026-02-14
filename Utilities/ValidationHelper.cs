using System.ComponentModel.DataAnnotations;

namespace FamilyBudgetExpenseTracker.Utilities
{
    public static class ValidationHelper
    {
        public static bool IsValidObject(object model, out List<ValidationResult> results)
        {
            var ctx = new ValidationContext(model);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, ctx, results, validateAllProperties: true);
        }
    }
}

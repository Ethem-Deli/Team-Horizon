using System.Globalization;

namespace FamilyBudgetExpenseTracker.Utilities
{
    public static class CurrencyHelper
    {
        public static string Format(decimal amount, string? symbol = null)
        {
            symbol ??= Constants.DefaultCurrencySymbol;
            return $"{symbol}{amount.ToString("N2", CultureInfo.InvariantCulture)}";
        }
    }
}

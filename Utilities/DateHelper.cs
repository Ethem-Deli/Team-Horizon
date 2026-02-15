namespace FamilyBudgetExpenseTracker.Utilities
{
    public static class DateHelper
    {
        public static string MonthName(int month)
            => new DateTime(2000, Math.Clamp(month, 1, 12), 1).ToString("MMMM");

        public static string MonthShortName(int month)
            => new DateTime(2000, Math.Clamp(month, 1, 12), 1).ToString("MMM");

        public static string MonthYearLabel(int year, int month)
            => new DateTime(year, Math.Clamp(month, 1, 12), 1).ToString("MMMM yyyy");
    }
}

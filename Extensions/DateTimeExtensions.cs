namespace FamilyBudgetExpenseTracker.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfMonth(this DateTime date)
            => new DateTime(date.Year, date.Month, 1);

        public static DateTime EndOfMonthExclusive(this DateTime date)
            => new DateTime(date.Year, date.Month, 1).AddMonths(1);

        public static string ToMonthYearLabel(this DateTime date)
            => date.ToString("MMMM yyyy");
    }
}

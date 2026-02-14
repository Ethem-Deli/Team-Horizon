namespace FamilyBudgetExpenseTracker.Services
{
    // Optional: used if you want to move report logic out of Razor pages.
    public interface IReportService
    {
        Task<decimal> GetYearTotalAsync(int userId, int year);
        Task<Dictionary<int, decimal>> GetMonthlyTotalsAsync(int userId, int year);
        // CategoryId can be null for uncategorized expenses in some schemas.
        // We normalize null to 0 in the service to keep dictionary keys non-null.
        Task<Dictionary<int, decimal>> GetCategoryTotalsAsync(int userId, int year);
    }
}

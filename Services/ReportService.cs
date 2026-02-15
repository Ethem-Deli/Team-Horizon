using FamilyBudgetExpenseTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudgetExpenseTracker.Services
{
    // Optional: used if you want to move report logic out of Razor pages.
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _db;

        public ReportService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<decimal> GetYearTotalAsync(int userId, int year)
        {
            if (userId <= 0) return 0m;

            var total = await _db.Expenses
                .AsNoTracking()
                .Where(e => e.UserId == userId && e.Date.Year == year)
                .SumAsync(e => (double?)e.Amount) ?? 0d;

            return (decimal)total;
        }

        public async Task<Dictionary<int, decimal>> GetMonthlyTotalsAsync(int userId, int year)
        {
            if (userId <= 0) return new();

            var raw = await _db.Expenses
                .AsNoTracking()
                .Where(e => e.UserId == userId && e.Date.Year == year)
                .GroupBy(e => e.Date.Month)
                .Select(g => new { Month = g.Key, Total = g.Sum(x => (double)x.Amount) })
                .ToListAsync();

            return raw.ToDictionary(x => x.Month, x => (decimal)x.Total);
        }

        public async Task<Dictionary<int, decimal>> GetCategoryTotalsAsync(int userId, int year)
        {
            if (userId <= 0) return new();

            var raw = await _db.Expenses
                .AsNoTracking()
                .Where(e => e.UserId == userId && e.Date.Year == year)
                .GroupBy(e => e.CategoryId)
                .Select(g => new { CategoryId = g.Key, Total = g.Sum(x => (double)x.Amount) })
                .ToListAsync();

            // The GroupBy key is already non-nullable int because CategoryId in the model is likely int.
            // If CategoryId is nullable, the Select projection handles the value.
            return raw.ToDictionary(x => x.CategoryId, x => (decimal)x.Total);
        }
    }
}

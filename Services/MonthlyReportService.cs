using Microsoft.EntityFrameworkCore;
using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Models.ViewModels;

namespace FamilyBudgetExpenseTracker.Services
{
    public class MonthlyReportService
    {
        private readonly ApplicationDbContext _db;

        public MonthlyReportService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<MonthlyReportViewModel> GetMonthlyReportAsync(int userId, int year, int month)
        {
            // SECURITY: If user is not logged in, return an empty/safe report.
            if (userId <= 0)
            {
                return new MonthlyReportViewModel
                {
                    Year = year,
                    Month = month,
                    TotalBudget = 0m,
                    TotalExpenses = 0m,
                    Expenses = new List<FamilyBudgetExpenseTracker.Models.Expense>(),
                    CategorySummaries = new List<MonthlyReportViewModel.CategorySummary>()
                };
            }

            // SECURITY: All report data MUST be filtered by UserId (user isolation).
            var expenses = await _db.Expenses
                .AsNoTracking()
                .Include(e => e.Category)
                .Where(e => e.UserId == userId && e.Date.Month == month && e.Date.Year == year)
                .ToListAsync();

            // SECURITY: Budget lookup is also filtered by UserId.
            var budget = await _db.Budgets
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Month == month && b.Year == year);

            // Build the report view model (safe to show in UI)
            var viewModel = new MonthlyReportViewModel
            {
                Year = year,
                Month = month,
                TotalBudget = budget?.Amount ?? 0m,
                TotalExpenses = expenses.Sum(e => e.Amount),
                Expenses = expenses
            };

            // Group by category name (LINQ to Objects)
            viewModel.CategorySummaries = expenses
                .GroupBy(e => e.Category?.Name ?? "Uncategorized")
                .Select(group => new MonthlyReportViewModel.CategorySummary
                {
                    CategoryName = group.Key,
                    TotalSpent = group.Sum(e => e.Amount),
                    TransactionCount = group.Count(),
                    // Guard against division by zero
                    PercentageOfTotalSpending = viewModel.TotalExpenses > 0
                        ? (double)(group.Sum(e => e.Amount) / viewModel.TotalExpenses) * 100
                        : 0
                })
                .OrderByDescending(c => c.TotalSpent)
                .ToList();

            return viewModel;
        }
    }
}

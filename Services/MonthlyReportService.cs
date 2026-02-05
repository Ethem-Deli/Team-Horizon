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
            // 1. Fetch the raw data
            var expenses = await _db.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId && e.Date.Month == month && e.Date.Year == year)
                .ToListAsync();

            var budget = await _db.Budgets
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Month == month && b.Year == year);

            // 2. Initialize the ViewModel
            var viewModel = new MonthlyReportViewModel
            {
                Year = year,
                Month = month,
                TotalBudget = budget?.Amount ?? 0m,
                TotalExpenses = expenses.Sum(e => e.Amount),
                Expenses = expenses
            };

            // 3. Perform the Category Grouping (LINQ to Objects)
            viewModel.CategorySummaries = expenses
                .GroupBy(e => e.Category?.Name ?? "Uncategorized")
                .Select(group => new MonthlyReportViewModel.CategorySummary
                {
                    CategoryName = group.Key,
                    TotalSpent = group.Sum(e => e.Amount),
                    TransactionCount = group.Count(),
                    // Calculate percentage of total spending (guard against div by zero)
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
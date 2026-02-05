using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyBudgetExpenseTracker.Models.ViewModels
{
    public class MonthlyReportViewModel
    {
        // 1. Report Metadata
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM");
        public string UserName { get; set; } = string.Empty;

        // 2. Financial Totals (Using decimal for precision)
        public decimal TotalBudget { get; set; }
        public decimal TotalExpenses { get; set; }

        // 3. Calculated Properties (The "Insights")
        public decimal RemainingBalance => TotalBudget - TotalExpenses;

        public bool IsOverBudget => TotalExpenses > TotalBudget;

        // Note: Explicit cast to double for the progress bar/percentage logic
        public double PercentageUsed => TotalBudget > 0
            ? (double)(TotalExpenses / TotalBudget) * 100
            : 0;

        // 4. Data Lists (Initialized to prevent NullReferenceExceptions)
        public List<Expense> Expenses { get; set; } = new();
        public List<CategorySummary> CategorySummaries { get; set; } = new();

        // 5. Nested class for the Category Breakdown table/chart
        public class CategorySummary
        {
            public string CategoryName { get; set; } = string.Empty;
            public decimal TotalSpent { get; set; }
            public int TransactionCount { get; set; }

            // Percentage of total monthly spending this category represents
            public double PercentageOfTotalSpending { get; set; }
        }
    }
}
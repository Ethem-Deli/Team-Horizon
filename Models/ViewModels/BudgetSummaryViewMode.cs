namespace FamilyBudgetExpenseTracker.Models.ViewModels
{
    // Note: the original filename used "ViewMode". Keeping it for compatibility.
    public class BudgetSummaryViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal SpentAmount { get; set; }

        public decimal RemainingAmount => BudgetAmount - SpentAmount;
        public decimal UsagePercent => BudgetAmount <= 0 ? 0 : (SpentAmount / BudgetAmount) * 100m;

        public string PeriodLabel => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
    }
}

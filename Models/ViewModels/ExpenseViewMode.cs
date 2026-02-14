namespace FamilyBudgetExpenseTracker.Models.ViewModels
{
    // Note: the original filename used "ViewMode". Keeping it for compatibility.
    public class ExpenseViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = "";

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = "Uncategorized";
        public string CategoryIcon { get; set; } = "";
        public string CategoryColor { get; set; } = "#000";

        public string DateLabel => Date.ToString("yyyy-MM-dd");
        public string AmountLabel => Amount.ToString("N2");
    }
}

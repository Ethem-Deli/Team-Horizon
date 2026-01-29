namespace FamilyBudgetExpenseTracker.Services;

public class UserState
{
    public int CurrentUserId { get; set; } = 1; // Demo user ID
    public string CurrentUserName { get; set; } = "Demo User";
    public bool IsAuthenticated { get; set; } = true; // For demo purposes
}
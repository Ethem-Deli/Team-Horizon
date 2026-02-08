using System.ComponentModel.DataAnnotations;


namespace FamilyBudgetExpenseTracker.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Full Name is required")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Email is required"), EmailAddress]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Username is required"), MinLength(3)]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "Password is required"), MinLength(6)]
    public string Password { get; set; } = "";
}
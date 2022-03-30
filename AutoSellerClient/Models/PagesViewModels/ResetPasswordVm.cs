using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.PagesViewModels;

public class ResetPasswordVm
{
    [Required]
    [EmailAddress]
    public string UserEmail { get; set; }

    [Required]
    public string ResetPasswordToken { get; set; }

    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
    
    [Required]
    [PasswordPropertyText]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
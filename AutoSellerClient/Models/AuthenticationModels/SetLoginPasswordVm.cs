using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.AuthenticationModels;

public class SetLoginPasswordVm
{
    [Required]
    [EmailAddress]
    public string ApplicationUserEmail { get; set; }
    
    [Required]
    [PasswordPropertyText]
    [Display(Name = "Current Password")]
    public string CurrentPassword { get; set; }

    [Required]
    [PasswordPropertyText]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; }

    [Required]
    [PasswordPropertyText]
    [Compare(nameof(NewPassword))]
    [Display(Name = "Confirm New Password")]
    public string ConfirmNewPassword { get; set; }

}
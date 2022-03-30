using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.AuthenticationModels;

public class SetUserPasswordDto
{
    [Required]
    [EmailAddress]
    public string ApplicationUserEmail { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string CurrentPassword { get; set; }

    [Required]
    [PasswordPropertyText]
    public string NewPassword { get; set; }

}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.AuthenticationModels;

public class ResetPasswordDto
{
    [Required]
    [EmailAddress]
    public string UserEmail { get; set; }

    [Required] 
    public string ResetPasswordToken { get; set; }

    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
}
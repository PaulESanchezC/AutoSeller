using System.ComponentModel.DataAnnotations;

namespace Models.AuthenticationModels;

public class ResetPasswordTokenValidatorDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string ResetPasswordToken { get; set; }
}
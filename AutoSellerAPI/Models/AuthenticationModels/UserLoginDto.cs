using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.AuthenticationModels;

public class UserLoginDto
{
    [Required]
    [EmailAddress]
    [Display(Name ="Email")]
    public string Username { get; set; }

    [Required]
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
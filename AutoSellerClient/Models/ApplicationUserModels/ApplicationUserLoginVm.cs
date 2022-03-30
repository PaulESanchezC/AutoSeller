using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.ApplicationUserModels;

public class ApplicationUserLoginVm
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Username { get; set; }

    [Required]
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string AccessToken { get; set; }
    public string ExternalLogin { get; set; }

    public string? ReturnUrl { get; set; } = null;
}
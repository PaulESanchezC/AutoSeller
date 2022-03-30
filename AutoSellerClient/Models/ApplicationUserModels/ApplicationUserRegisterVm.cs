using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.ApplicationUserModels;

public class ApplicationUserRegisterVm
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "First name is a required field")]
    [StringLength(100)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is a required field")]
    [StringLength(100)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is a required field")]
    [StringLength(100)]
    [Display(Name = "Phone")]
    public string PhoneNumber { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Address is a required field")]
    [StringLength(300)]
    public string Address { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "City is a required field")]
    [StringLength(100)]
    public string City { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "State/Province is a required field")]
    [StringLength(100)]
    [Display(Name = "State or Province")]
    public string StateOrProvince { get; set; }
    
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Username { get; set; }

    [Required]
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
    public string Password { get; set; }

    [Required]
    [PasswordPropertyText]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare(nameof(Password))]
    public string ConfirmPaswword { get; set; }

    public string ExternalLogin { get; set; }
    public string AccessToken { get; set; }
}
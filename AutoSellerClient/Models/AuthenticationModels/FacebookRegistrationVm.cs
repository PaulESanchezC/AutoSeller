using System.ComponentModel.DataAnnotations;

namespace Models.AuthenticationModels;

public class FacebookRegistrationVm
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
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Address is a required field")]
    [StringLength(300)]
    public string Address { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "City is a required field")]
    [StringLength(100)]
    public string City { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "State/Province is a required field")]
    [StringLength(100)]
    public string StateOrProvince { get; set; }
}
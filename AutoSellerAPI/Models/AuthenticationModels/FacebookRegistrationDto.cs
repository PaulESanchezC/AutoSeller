using System.ComponentModel.DataAnnotations;

namespace Models.AuthenticationModels;

public class FacebookRegistrationDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "First name is a required field")]
    [StringLength(100)]
    public string FirstName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is a required field")]
    [StringLength(100)]
    public string LastName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is a required field")]
    [StringLength(100)]
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
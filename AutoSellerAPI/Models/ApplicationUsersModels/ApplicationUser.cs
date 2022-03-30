using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Models.IntentsModels;
using Models.VehiclesModels;

namespace Models.ApplicationUsersModels;

public class ApplicationUser : IdentityUser 
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "First name is a required field")]
    [StringLength(100)]
    public string FirstName { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is a required field")]
    [StringLength(100)]
    public string LastName { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Address is a required field")]
    [StringLength(300)]
    public string Address { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "City is a required field")]
    [StringLength(100)]
    public string City { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "State/Province is a required field")]
    [StringLength(100)]
    public string StateOrProvince { get; set; }

    public List<Intent> IntentsSent { get; set; }
    public List<Intent> IntentsRecieved { get; set; }
    public List<Vehicle> VehiclesListed { get; set; }

    
}
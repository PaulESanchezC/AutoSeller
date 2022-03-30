using System.ComponentModel.DataAnnotations;
using Models.MakerModels;
using Models.VehicleModels;

namespace Models.ListedVehicleModels;

public class ListedVehicleCreateVm
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Transmission is required")]
    [StringLength(20)]
    public string Transmission { get; set; } = "";

    [Required(AllowEmptyStrings = false, ErrorMessage = "Color is required")]
    [StringLength(100)]
    [Display(Name = "Vehicle's Color")]
    public string Color { get; set; } = "";

    [Required(AllowEmptyStrings = false, ErrorMessage = "Drive train is required")]
    [StringLength(10)]
    public string DriveTrain { get; set; } = "";

    [Required(AllowEmptyStrings = false, ErrorMessage = "Year is required")]
    [Range(1960, int.MaxValue)]
    public int Year { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Mileage is required")]
    [Range(100, int.MaxValue)]
    [Display(Name = "Mileage(x1000 Kms)")]
    public int Mileage { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Price is required")]
    [Range(1000, int.MaxValue, ErrorMessage = "the minimum price accepted is CA$ 1000")]
    [Display(Name = "Price")]
    public double Price { get; set; }

    [StringLength(500)]
    [Display(Name = "Vehicle's Description")]
    public string Description { get; set; } = "";

    //Vehicle Model
    [Required(AllowEmptyStrings = false, ErrorMessage = "Vehicle is required")]
    [Display(Name = "Vehicle Model")]
    public string VehicleId { get; set; } = "";

    //Application User
    [Required(AllowEmptyStrings = false, ErrorMessage = "User is required")]
    public string ApplicationUserId { get; set; } = "";
}
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Models.MakerModels;

namespace Models.VehiclesModels;

[Index(nameof(VehicleName),IsUnique = true)]
public class Vehicle
{
    [Key]
    public string VehicleId { get; set; } = Guid.NewGuid().ToString();

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name field is required")]
    [StringLength(100)]
    public string VehicleName { get; set; }

    [Required]
    public string MakerId { get; set; }
    public Maker Maker { get; set; }

}
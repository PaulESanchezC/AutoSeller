using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.MakerModels;

namespace Models.VehiclesModels;

public class VehicleCreateDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name field is required")]
    [StringLength(100)]
    public string VehicleName { get; set; }

    [Required]
    public string MakerId { get; set; }
}
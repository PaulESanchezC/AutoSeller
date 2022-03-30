using System.ComponentModel.DataAnnotations;
using Models.MakerModels;

namespace Models.VehiclesModels;

public class VehicleDto
{
    public string VehicleId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name field is required")]
    [StringLength(100)]
    public string VehicleName { get; set; }

    [Required]
    public string MakerId { get; set; }
    public MakerDto Maker { get; set; }
}
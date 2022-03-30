using System.ComponentModel.DataAnnotations;
using Models.IntentsModels;

namespace Models.ListedVehiclesModels;

public class ListedVehicleSoldDto
{
    [Required] public ListedVehicleDto ListedVehicle { get; set; } = new ListedVehicleDto();

    [Required] public IntentDto Intent { get; set; } = new IntentDto();
}
using System.ComponentModel.DataAnnotations;
using Models.VehiclesModels;

namespace Models.MakerModels;

public class MakerDto
{
    public string MakerId { get; set; }
    
    public string MakerName { get; set; }

    public IEnumerable<VehicleDto> Vehicles { get; set; }

}
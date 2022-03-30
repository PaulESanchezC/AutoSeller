using Models.MakerModels;

namespace Models.VehicleModels;

public class Vehicles
{
    public string VehicleId { get; set; }

    
    public string VehicleName { get; set; }

    
    public string MakerId { get; set; }
    public Makers Maker { get; set; }
}
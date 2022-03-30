using Models.MakerModels;

namespace Models.VehicleModels;

public class VehiclesCreateVm
{
    public string VehicleName { get; set; }
    public string MakerId { get; set; }
    public Makers Maker { get; set; }
}
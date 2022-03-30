using Models.VehicleModels;

namespace Models.MakerModels;

public class Makers
{
    public string MakerId { get; set; }

    public string MakerName { get; set; }

    public IEnumerable<Vehicles> Vehicles { get; set; }
}
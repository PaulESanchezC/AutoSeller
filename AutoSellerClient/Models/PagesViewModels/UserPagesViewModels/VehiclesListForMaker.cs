using Models.VehicleModels;

namespace Models.PagesViewModels.UserPagesViewModels;

public class VehiclesListForMaker
{
    public string MakerId { get; set; }
    public string MakerName { get; set; }
    public List<Vehicles> Vehicles { get; set; }
}
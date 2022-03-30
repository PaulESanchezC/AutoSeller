using Models.ListedVehicleModels;
using Models.MakerModels;


namespace Models.PagesViewModels;

public class IndexPageVm
{
    public IEnumerable<ListedVehicle> ListedVehiclesList { get; set; } = new List<ListedVehicle>();
    public IEnumerable<Makers> ListOfMakers { get; set; } = new List<Makers>();
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int Count { get; set; }
}
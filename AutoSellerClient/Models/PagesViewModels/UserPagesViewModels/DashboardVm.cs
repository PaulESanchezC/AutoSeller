using Models.ApplicationUserModels;
using Models.IntentModels;
using Models.ListedVehicleModels;

namespace Models.PagesViewModels.UserPagesViewModels;

public class DashboardVm
{
    public ApplicationUser ApplicationUser { get; set; }
    public List<ListedVehicle> ListedVehicles { get; set; }
    public List<ListedVehicleSoldVm> SoldListedVehicles { get; set; }
}
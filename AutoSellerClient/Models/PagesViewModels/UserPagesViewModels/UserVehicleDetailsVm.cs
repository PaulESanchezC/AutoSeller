using Models.ImagesModels;
using Models.ListedVehicleModels;

namespace Models.PagesViewModels.UserPagesViewModels;

public class UserVehicleDetailsVm
{
    public ListedVehicleUpdateVm? ListedVehicleUpdateVm { get; set; } = new ListedVehicleUpdateVm();
    public ICollection<Images>? Images { get; set; } = new List<Images>();
    public ListedVehicle? ListedVehicle { get; set; } = new ListedVehicle();
}
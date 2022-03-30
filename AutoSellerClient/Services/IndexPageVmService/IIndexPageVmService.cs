using Models.ListedVehicleModels;
using Models.MakerModels;
using Models.PagesViewModels;

namespace Services.IndexPageVnService;

public interface IIndexPageVmService
{
    Task<IndexPageVm> GetIndexPageVmAsync(IEnumerable<ListedVehicle> vehiclesList, IEnumerable<Makers> listOfMakers, int totalResponseObjects, int pageSize, int currentPage);
}
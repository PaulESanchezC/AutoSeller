using Models.ListedVehicleModels;
using Models.MakerModels;
using Models.PagesViewModels;

namespace Services.IndexPageVnService;

public class IndexPageVmService : IIndexPageVmService
{
    public Task<IndexPageVm> GetIndexPageVmAsync(IEnumerable<ListedVehicle> listedVehicles, IEnumerable<Makers> listOfMakers, int totalResponseObjects, int pageSize, int currentPage)
    {
        var totalPages = (int)Math.Ceiling(decimal.Divide(totalResponseObjects, pageSize));
        currentPage = currentPage > totalPages? totalPages:currentPage;

        var indexPageVm = new IndexPageVm() {
            PageSize = pageSize,
            ListedVehiclesList = listedVehicles,
            ListOfMakers = listOfMakers,
            Count = totalResponseObjects,
            TotalPages = totalPages,
            CurrentPage = currentPage
        };
        return Task.FromResult(indexPageVm);
    }
}
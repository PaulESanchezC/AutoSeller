using Models.ListedVehicleModels;
using Models.PagesViewModels;
using Models.ResponseModels;
using Services.RepositoryServices.CrudRepository;

namespace Services.RepositoryServices.ListedVehiclesRepository;

public interface IListedVehicleRepository : ICrudRepository<ListedVehicle, ListedVehicleCreateVm, ListedVehicleUpdateVm>
{
    Task<IList<ListedVehicle>> MapManyListedVehiclesAsync(object vehiclesList);
    Task<ListedVehicle> MapSingleListedVehicleAsync(object vehicle);
    Task<Response> GetAllBySearchAsync(SearchVm searchByVm);
}
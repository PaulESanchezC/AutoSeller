using Models.VehicleModels;

using Services.RepositoryServices.CrudRepository;

namespace Services.RepositoryServices.VehiclesRepository;

public interface IVehiclesRepository : ICrudRepository<Vehicles,VehiclesCreateVm,VehiclesUpdateVm>
{
    Task<IEnumerable<Vehicles>> MapManyVehiclesAsync(object vehiclesList);
    Task<Vehicles> MapSingleVehicleAsync(object vehicle);
    Task<string> MapManyVehiclesFromAMakerAsync(object makerVehicleList);
}
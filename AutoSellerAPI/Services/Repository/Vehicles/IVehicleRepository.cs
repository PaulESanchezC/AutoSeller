using Models.VehiclesModels;

namespace Services.Repository.Vehicles;

public interface IVehicleRepository : ICrudRepository<Vehicle, VehicleDto,VehicleCreateDto, VehicleUpdatetDto>
{
}
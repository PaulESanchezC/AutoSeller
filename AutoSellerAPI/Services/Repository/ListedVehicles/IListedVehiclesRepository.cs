using Models.ListedVehiclesModels;
using Models.ResponseModels;

namespace Services.Repository.ListedVehicles;

public interface IListedVehiclesRepository : ICrudRepository<ListedVehicle,ListedVehicleDto,ListedVehicleCreateDto, ListedVehicleUpdateDto>
{
    Task<Response> SetVehicleAsSoldAsync(string intentId, string applicationUserId,CancellationToken cancellationToken);
    Task<Response> ValidateDriveTrainAndTransmissionAsync(string driveTrain, string Transmission, CancellationToken cancellationToken);
    Task<Response> GetListedVehicleByVehicleModelNameAsync(string modelName, CancellationToken cancellationToken);
    Task<Response> GetAllListedVehiclesBySearchAsync(ListedVehicleSearchModelDto listedVehicleSearchDto, CancellationToken cancellationToken);
    Task<Response> MapManySoldListedVehiclesAsync(object response, CancellationToken cancellationToken);
    Task<Response> MapSingleSoldListedVehicleAsync(object responseObject, CancellationToken cancellationToken);
    Task<Response> SetListedVehicleToDeletedAsync(string listedVehicleId, CancellationToken cancellationToken);
}
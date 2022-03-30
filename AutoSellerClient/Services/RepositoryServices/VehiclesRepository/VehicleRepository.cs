using AutoMapper;
using Models.PagesViewModels.UserPagesViewModels;
using Models.VehicleModels;
using Newtonsoft.Json;
using Services.RepositoryServices.CrudRepository;

namespace Services.RepositoryServices.VehiclesRepository;

public class VehicleRepository : CrudRepository<Vehicles,VehiclesCreateVm,VehiclesUpdateVm>, IVehiclesRepository
{
    private readonly IMapper _mapper;
    public VehicleRepository(HttpClient httpClient, IMapper mapper) : base(httpClient, mapper)
    {
        _mapper = mapper;
    }


    public Task<IEnumerable<Vehicles>> MapManyVehiclesAsync(object vehiclesList)
    {
        var mappedVehicleList = _mapper.Map<IEnumerable<Vehicles>>(vehiclesList);
        return Task.FromResult(mappedVehicleList);
    }
    public Task<Vehicles> MapSingleVehicleAsync(object vehicle)
    {
        var mappedVehicle = _mapper.Map<Vehicles>(vehicle);
        return Task.FromResult(mappedVehicle);
    }
    public Task<string> MapManyVehiclesFromAMakerAsync(object makerVehicleList)
    {
        var vehiclesList = _mapper.Map<IEnumerable<VehiclesListForMaker>>(makerVehicleList);
        var vehicles = vehiclesList.Select(v=>v.Vehicles).FirstOrDefault();
        return Task.FromResult(JsonConvert.SerializeObject(vehicles));
    }
}
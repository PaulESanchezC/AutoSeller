using AutoMapper;
using Data;
using Models.VehiclesModels;

namespace Services.Repository.Vehicles;

public class VehicleRepository : CrudRepository<Vehicle,VehicleDto,VehicleCreateDto,VehicleUpdatetDto>, IVehicleRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    public VehicleRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }
}
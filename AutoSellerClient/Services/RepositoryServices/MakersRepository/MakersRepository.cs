using AutoMapper;
using Models.MakerModels;
using Services.RepositoryServices.CrudRepository;

namespace Services.RepositoryServices.MakersRepository;

public class MakersRepository : CrudRepository<Makers,MakersCreateVm,MakersUpdateVm>, IMakersRepository
{
    private readonly IMapper _mapper;
    public MakersRepository(HttpClient httpClient, IMapper mapper) : base(httpClient, mapper)
    {
        _mapper = mapper;
    }

    public Task<IEnumerable<Makers>> MapeManyMakersAsync(object makersObjectsList)
    {
        var makersMapped = _mapper.Map<IEnumerable<Makers>>(makersObjectsList);
        return Task.FromResult(makersMapped);
    }
}
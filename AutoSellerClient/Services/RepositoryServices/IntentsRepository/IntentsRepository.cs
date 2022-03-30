using AutoMapper;
using Models.IntentModels;
using Services.RepositoryServices.CrudRepository;

namespace Services.RepositoryServices.IntentsRepository;

public class IntentsRepository : CrudRepository<Intents, IntentsCreateVm, IntentsUpdateVm>, IIntentRepository
{
    public IntentsRepository(HttpClient httpClient, IMapper mapper) : base(httpClient, mapper)
    {
    }
}
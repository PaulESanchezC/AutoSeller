using AutoMapper;
using Models.ApplicationUserModels;

namespace Services.RepositoryServices.AplicationUsesrRepository;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly IMapper _mapper;

    public ApplicationUserRepository(IMapper mapper)
    {
        _mapper = mapper;
    }


    public Task<ApplicationUser> MapApplicationUserFromObject(object objectToMap)
    {
        return Task.FromResult(_mapper.Map<ApplicationUser>(objectToMap));
    }
}
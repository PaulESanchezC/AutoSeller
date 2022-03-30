using Models.ApplicationUserModels;
using Models.ResponseModels;

namespace Services.RepositoryServices.AplicationUsesrRepository;

public interface IApplicationUserRepository
{
    Task<ApplicationUser> MapApplicationUserFromObject(object objectToMap);
}
using Models.IntentModels;
using Services.RepositoryServices.CrudRepository;

namespace Services.RepositoryServices.IntentsRepository;

public interface IIntentRepository : ICrudRepository<Intents, IntentsCreateVm, IntentsUpdateVm>
{
    
}
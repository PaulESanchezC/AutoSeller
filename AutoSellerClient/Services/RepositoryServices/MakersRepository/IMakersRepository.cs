using Models.MakerModels;
using Services.RepositoryServices.CrudRepository;

namespace Services.RepositoryServices.MakersRepository
{
    public interface IMakersRepository : ICrudRepository<Makers,MakersCreateVm,MakersUpdateVm>
    {
        Task<IEnumerable<Makers>> MapeManyMakersAsync(object makersObjectsList);
    }
}
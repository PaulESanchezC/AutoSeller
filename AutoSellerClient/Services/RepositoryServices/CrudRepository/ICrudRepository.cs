using Models.ResponseModels;

namespace Services.RepositoryServices.CrudRepository;

public interface ICrudRepository<T,TCreate,TUpdate> 
    where T : class
    where TCreate : class
    where TUpdate : class
{
    Task<Response?> GetAll(string apiRoute,string jwtToken=null);
    Task<Response?> GetAllByPages(string apiRoute, int pageSize, int currentPage);
    Task<Response> Get(string apiRoute, string? jwtToken = null);
    Task<T> Create(TCreate objectToCreate);
    Task<Response> Update(TUpdate objectToUpdate, string apiRoute, string? jwtToken = null);
    Task<bool> Delete(string objectTODeleteId, string apiRoute,string? jwtToken = null);
}
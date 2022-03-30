using System.Linq.Expressions;
using Models.ResponseModels;

namespace Services.Repository;

public interface ICrudRepository<T, TDto, TCreateDto, TUpdatetDto>
    where T : class
    where TDto : class
    where TCreateDto : class
    where TUpdatetDto : class
{
    Task<Response> GetAllByAsync(Expression<Func<T, bool>>? predicate, Expression<Func<T, object>>? orderBy, CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes);
    Task<Response> GetAllWithPagesAsync(int pageSize, int currentPage, Expression<Func<T, bool>>? predicate, Expression<Func<T, object>>? orderBy, CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes);

    Task<Response> GetSingleByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes);

    Task<Response> CreateAsync(TCreateDto objectToCreateDto, CancellationToken cancellationToken);

    Task<Response> UpdateAsync(TUpdatetDto objectToUpdateDto, CancellationToken cancellationToken);

    Task<Response> DeleteAsync(string objectToDeleteId, CancellationToken cancellationToken);
}
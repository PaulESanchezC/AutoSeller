using System.Linq.Expressions;
using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.ResponseModels;

namespace Services.Repository;

public class CrudRepository<T, TDto, TCreateDto, TUpdatetDto> : ICrudRepository<T, TDto, TCreateDto, TUpdatetDto>
    where T : class
    where TDto : class
    where TCreateDto : class
    where TUpdatetDto : class
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CrudRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Response> GetAllByAsync(Expression<Func<T, bool>>? predicate, Expression<Func<T, object>>? orderBy, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
    {
        var query = _db.Set<T>()
            .AsQueryable()
            .AsNoTracking();

        var result = includes.Aggregate(query, (result, data) => result.Include(data));
        if (predicate != null)
            result = result.Where(predicate);
        if (orderBy != null)
            result = result.OrderBy(orderBy);

        if (!result.Any())
            return await ResponseCreatorAsync(null, 0, false, "Empty result", "Operation was successful, but returned empty!", 400);

        var resultDto = _mapper.Map<IEnumerable<TDto>>(await result.ToListAsync(cancellationToken));
        return await ResponseCreatorAsync(resultDto, query.Count(), true, "Operation Successful", "No problem!", 200);
    }

    public async Task<Response> GetAllWithPagesAsync(int pageSize, int currentPage, Expression<Func<T, bool>>? predicate, Expression<Func<T, object>>? orderBy,
        CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
    {
        var query = _db.Set<T>()
            .AsQueryable()
            .AsNoTracking();
            
        var result = includes.Aggregate(query, (result, data) => result.Include(data));
        if (predicate != null)
            result = result.Where(predicate);
        if (orderBy != null)
            result = result.OrderBy(orderBy);
        
        var pagination = result.Skip((currentPage - 1) * pageSize).Take(pageSize);

        if (!pagination.Any())
            return await ResponseCreatorAsync(null, 0, false, "Empty result", "Operation was successful, but returned empty!", 400);

        var resultDto = _mapper.Map<IEnumerable<TDto>>(await pagination.ToListAsync(cancellationToken));
        return await ResponseCreatorAsync(resultDto, result.Count() , true, "Operation Successful", "No problem!", 200);
    }

    public async Task<Response> GetSingleByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
    {
        var query = _db.Set<T>()
            .Where(predicate)
            .AsQueryable()
            .AsNoTracking();

        var result = includes.Aggregate(query, (result, data) => result.Include(data));

        if (!result.Any())
            return await ResponseCreatorAsync(null, 0, false, "Empty result", "Operation was successful, but returned empty!", 400);

        var resultDto = _mapper.Map<TDto>(await result.FirstOrDefaultAsync(cancellationToken));
        return await ResponseCreatorAsync(resultDto, query.Count(), true, "Operation Successful", "No problem!", 200);
    }

    public async Task<Response> CreateAsync(TCreateDto objectToCreateDto, CancellationToken cancellationToken)
    {
        var objectToCreate = _mapper.Map<T>(objectToCreateDto);

        var entity = await _db.AddAsync<T>(objectToCreate, cancellationToken);
        if (entity.State != EntityState.Added)
            return await ResponseCreatorAsync(objectToCreateDto, 0, false, "Error Creating the object", $"The entity state should be EntityState.Added but is, {entity.State}", 520);
        int result = 0;
        try
        {
            result = await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e.Message);
            return await ResponseCreatorAsync(objectToCreateDto, 0, false, "Error Saving", $"Error saving the {typeof(T).Name}", 409);
        }

        var responseObjectDto = _mapper.Map<TDto>(objectToCreate);
        return await ResponseCreatorAsync(responseObjectDto, result, true, "Operation successful", $"The {typeof(T).Name} was created and saved successfully", 200);
    }

    public async Task<Response> UpdateAsync(TUpdatetDto objectToUpdateDto, CancellationToken cancellationToken)
    {
        var objectToUpdate = _mapper.Map<T>(objectToUpdateDto);
        
        _db.ChangeTracker.Clear();
        var entity = _db.Update(objectToUpdate);

        if (entity.State != EntityState.Modified)
            return await ResponseCreatorAsync(null, 0, false, "Error updating the object", $"The entity state should be EntityState.Modified but is, {entity.State}", 520);
        
        var result = 0;
        try
        {
            result = await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            return await ResponseCreatorAsync(null, 0,false, "Error updating the object", $"{typeof(T).Name} could not be updated", 409);
        }

        var responseObjectDto = _mapper.Map<TDto>(objectToUpdate);
        return await ResponseCreatorAsync(responseObjectDto, result, true, "Operation successful", $"The {typeof(T).Name} was updated and saved successfully", 200);
    }

    public async Task<Response> DeleteAsync(string objectToDeleteId, CancellationToken cancellationToken)
    {
        var objectToDelete = await _db.FindAsync<T>(objectToDeleteId);

        if (objectToDelete == null)
            return await ResponseCreatorAsync(null, 0, false, "Could not Delete", $"Could not find the {typeof(T).Name} to delete by the Id: {objectToDeleteId}", 400);

        var entity = _db.Remove(objectToDelete);
        if (entity.State != EntityState.Deleted)
            return await ResponseCreatorAsync(null, 0, false, "Error Deleting the object", $"The entity state should be EntityState.Deleted but is, {entity.State}", 520);
        var result = 0;
        try
        {
            result = await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            return await ResponseCreatorAsync(null, 0, false, "Error Deleting the object", $"{typeof(T).Name} was found, but could not be deleted", 409);
        }
        return await ResponseCreatorAsync(null, result, true, "Operation successful", $"The {typeof(T).Name} was Deleted successfully", 200);
    }

    private Task<Response> ResponseCreatorAsync(object? responseObjectDto, int totalResponseCount, bool isSuccesful, string title, string message, int statusCode)
    {
        var response = new Response
        {
            Title = title,
            IsSuccessful = isSuccesful,
            Message = message,
            StatusCode = statusCode,
            TotalResponseCount = totalResponseCount,
            ResponseObject = responseObjectDto
        };
        return Task.FromResult(response);
    }
}
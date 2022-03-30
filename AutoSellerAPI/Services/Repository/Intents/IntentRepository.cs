using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.IntentsModels;
using Models.ResponseModels;

namespace Services.Repository.Intents;

public class IntentRepository : CrudRepository<Intent,IntentDto,IntentCreateDto,IntentUpdateDto>, IIntentsRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;
    public IntentRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Response> ValidateIntentIsUniqueForUser(IntentCreateDto intentCreateDto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (intentCreateDto.IntentReceiverId == intentCreateDto.IntentSenderId)
            return await CreateResponse(false, 409, "Invalid Operation", "The Buyer cannot be the same as the seller",
                null);
        
        var intent = await _db.Intents.FirstOrDefaultAsync(i => i
            .IntentSenderId == intentCreateDto.IntentSenderId && i.IntentReceiverId == intentCreateDto.IntentReceiverId
            && i.ListedVehicleId == intentCreateDto.ListedVehicleId && i.IsSold ==false && i.IsDiscarded == false, cancellationToken);
        if(intent != null)
            return await CreateResponse(false, 409, "Invalid Operation", "You've already informed the seller of your interest. thank you",
                null);
        return await CreateResponse(true, 200, "Ok", "Ok", null);
    }

    public async Task<Response> GetIntentsForListedVehicleByModelName(string modelName, CancellationToken cancellationToken)
    {
        var intents = await _db.Intents.Include(i => i.ListedVehicle).Include(i => i.ListedVehicle.Vehicle)
            .Where(i => i.ListedVehicle.Vehicle.VehicleName == modelName.ToUpper().Trim()).ToListAsync(cancellationToken);
        if (!intents.Any())
            return await CreateResponse(false, 520, "Empty result!", "Operation successful but returned empty result",
                modelName);

        var intentDtos = _mapper.Map<IEnumerable<IntentDto>>(intents);
        return await CreateResponse(true, 200, "Operation successful", "Operation successful", intentDtos);
    }

    public async Task<Response> ToggleIntentIsReadAsync(string intentId, string applicationUserId, CancellationToken cancellationToken)
    {
        var intent = await _db.Intents.FindAsync(intentId);
        if (intent is null)
            return await CreateResponse(false,404,"Not Found","The intent could not be found",null);

        if (intent.IntentReceiverId != applicationUserId)
            return await CreateResponse(false, 409, "Not Found", "The intent could not be found", null);

        intent.IsRead = !intent.IsRead;
        _db.ChangeTracker.Clear();
        var entity = _db.Intents.Update(intent);
        if (entity.State != EntityState.Modified)
            return await CreateResponse(false, 405, "Could not update", "An error occurred while trying to update the intent", null);

        try
        {
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
        {
            return await CreateResponse(false, 405, "Could not save", $"An error occurred while trying to save the intent, Error Type of {e.Message}", null);
        }

        return await CreateResponse(false, 200, "Ok", "Ok", intent.IsRead);
    }

    public async Task<Response> SetIntentAsDiscarded(string intentId, string applicationUserId, CancellationToken cancellationToken)
    {
        var intent = await _db.Intents.FindAsync(intentId);
        if (intent is null)
            return await CreateResponse(false, 404, "Not Found", "The intent could not be found", null);

        if (intent.IntentReceiverId != applicationUserId)
            return await CreateResponse(false, 409, "Not Found", "The intent could not be found", null);

        intent.IsDiscarded = true;
        _db.ChangeTracker.Clear();
        var entity = _db.Intents.Update(intent);
        if (entity.State != EntityState.Modified)
            return await CreateResponse(false, 405, "Could not update", "An error occurred while trying to update the intent", null);

        try
        {
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
        {
            return await CreateResponse(false, 405, "Could not save", $"An error occurred while trying to save the intent, Error Type of {e.Message}", null);
        }

        return await CreateResponse(false, 200, "Ok", "", null);
    }

    //Helper Methods
    private Task<Response> CreateResponse(bool isSuccessful, int statusCode, string title, string message, object? responseObject)
    {
        return Task.FromResult(new Response()
        {
            IsSuccessful = isSuccessful,
            StatusCode = statusCode,
            Title = title,
            Message = message,
            ResponseObject = responseObject
        });
    }
}
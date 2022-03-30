using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.IntentsModels;
using Models.ListedVehiclesModels;
using Models.ResponseModels;
using Newtonsoft.Json;

namespace Services.Repository.ListedVehicles;

public class ListedVehiclesRepository : CrudRepository<ListedVehicle, ListedVehicleDto, ListedVehicleCreateDto, ListedVehicleUpdateDto>, IListedVehiclesRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;

    public ListedVehiclesRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Response> SetVehicleAsSoldAsync(string intentId, string applicationUserId, CancellationToken cancellationToken)
    {
        var intent = await _db.Intents
            .Include(i => i.ListedVehicle)
            .Include(i => i.IntentReceiver)
            .FirstOrDefaultAsync(i => i.IntentId == intentId, cancellationToken);

        if (intent == null)
            return await CreateResponse(false, 0, 409, "Intent doesn't exist",
                "Unable to find the Intent for the Vehicle with the provided Id", null);

        intent.IsSold = true;
        intent.DateSold = DateTime.Now;

        var dbListedVehicle = await _db.ListedVehicles.FirstOrDefaultAsync(lv => lv.ListedVehicleId == intent.ListedVehicleId, cancellationToken);
        if (dbListedVehicle == null)
            return await CreateResponse(false, 0, 409, "Listed Vehicle doesn't exist",
                "Unable to find the listed Vehicle with the provided Id", null);

        if (intent.IntentId != intentId || intent.IntentReceiverId != dbListedVehicle.ApplicationUserId || applicationUserId != dbListedVehicle.ApplicationUserId)
            return await CreateResponse(false, 0, 400, "Invalid Operation",
                "Unable to get results", null);

        dbListedVehicle.IsSold = true;
        dbListedVehicle.DateSold = DateTime.Now;

        _db.ChangeTracker.Clear();

        var intentEntity = _db.Update(intent);
        if (intentEntity.State != EntityState.Modified)
            return await CreateResponse(false, 0, 409, "Error Updating", "Unable to Update the Intent", null);

        var entity = _db.Update(dbListedVehicle);
        if (entity.State != EntityState.Modified)
            return await CreateResponse(false, 0, 520, "Error Updating", "Unable to Update the listed Vehicle", null);

        var result = await _db.SaveChangesAsync(cancellationToken);

        if (result > 1)
        {
            var newListedVehicle = await _db.ListedVehicles
                .Include(i => i.Vehicle)
                .Include(i => i.Vehicle.Maker)
                .Include(i => i.ApplicationUser)
                .Include(i => i.Images.OrderBy(x => x.ImageIndex))
                .FirstOrDefaultAsync(l => l.ListedVehicleId == dbListedVehicle.ListedVehicleId && l.IsSold == true, cancellationToken);
            var listedVehicleDto = _mapper.Map<ListedVehicleDto>(newListedVehicle);
            return await CreateResponse(true, 1, 200, "Update successful", "Update successful", listedVehicleDto);
        }

        return await CreateResponse(false, 0, 409, "Error Saving", "Unable to Save the listed Vehicle", null);
    }

    public async Task<Response> ValidateDriveTrainAndTransmissionAsync(string driveTrain, string transmission, CancellationToken cancellationToken)
    {
        var transmissionValidation = await ValidateTransmission(transmission, cancellationToken);
        if (!transmissionValidation.IsSuccessful)
            return transmissionValidation;

        var driveTrainValidation = await ValidateDriveTrain(driveTrain, cancellationToken);
        if (!driveTrainValidation.IsSuccessful)
            return driveTrainValidation;

        return await CreateResponse(true, 0, 200, "Correct", "correct", null);
    }

    public async Task<Response> GetListedVehicleByVehicleModelNameAsync(string modelName, CancellationToken cancellationToken)
    {
        var listedVehicles = await _db.ListedVehicles
            .Include(l => l.Vehicle).Include(l => l.Vehicle.Maker).Include(l => l.ApplicationUser).Include(l => l.Images.OrderBy(i => i.ImageIndex))
            .Where(l => l.Vehicle.VehicleName == modelName.ToUpper().Trim() && l.IsSold == false && l.IsDeleted == false).ToListAsync(cancellationToken);

        if (!listedVehicles.Any())
            return await CreateResponse(false, 0, 520, "Empty result", "Operation is successful but return empty result", modelName);

        var listedVehicleDto = _mapper.Map<IEnumerable<ListedVehicleDto>>(listedVehicles);
        return await CreateResponse(true, listedVehicleDto.Count(), 200, "Operation Successful", "no problem!", listedVehicleDto);
    }

    public async Task<Response> GetAllListedVehiclesBySearchAsync(ListedVehicleSearchModelDto listedVehicleSearchDto,
        CancellationToken cancellationToken)
    {
        var query = await _db.ListedVehicles.Include(l => l.Vehicle).Include(l => l.Vehicle.Maker)
            .Include(l => l.ApplicationUser).Include(l => l.Images.OrderBy(i => i.ImageIndex)).Where(l => l.IsSold == false && l.IsDeleted == false).ToListAsync(cancellationToken);

        var listedVehicles = query.OrderBy(l => l.DateListed).ToList();

        if (!string.IsNullOrEmpty(listedVehicleSearchDto.MakerName))
            listedVehicles = listedVehicles.Where(l => l.Vehicle.Maker.MakerName == listedVehicleSearchDto.MakerName.ToUpper().Trim()).ToList();
        if (listedVehicleSearchDto.MaxYear != 0)
            listedVehicles = listedVehicles.Where(l => l.Year <= listedVehicleSearchDto.MaxYear).ToList();
        if (listedVehicleSearchDto.MaxPrice != 0)
            listedVehicles = listedVehicles.Where(l => l.Price <= listedVehicleSearchDto.MaxPrice).ToList();
        if (listedVehicleSearchDto.Mileage != 0)
            listedVehicles = listedVehicles.Where(l => l.Mileage <= listedVehicleSearchDto.Mileage).ToList();
        if (!string.IsNullOrEmpty(listedVehicleSearchDto.Transmission))
            listedVehicles = listedVehicles.Where(l => l.Transmission.ToUpper().Trim() == listedVehicleSearchDto.Transmission.ToUpper().Trim()).ToList();
        if (!string.IsNullOrEmpty(listedVehicleSearchDto.DriveTrain))
            listedVehicles = listedVehicles.Where(l => l.DriveTrain.ToUpper().Trim() == listedVehicleSearchDto.DriveTrain.ToUpper().Trim()).ToList();
        if (!string.IsNullOrEmpty(listedVehicleSearchDto.Color))
            listedVehicles = listedVehicles.Where(l => l.Color.ToUpper().Trim() == listedVehicleSearchDto.Color.ToUpper().Trim()).ToList();

        if (!listedVehicles.Any())
            return await CreateResponse(false, 0, 520, "Empty result", "Operation is successful but return empty result", listedVehicleSearchDto);

        if (listedVehicleSearchDto.PageSize < 1)
            listedVehicleSearchDto.PageSize = 4;

        var listedVehicleCount = listedVehicles.Count();
        var amountOfPages = Math.Ceiling(decimal.Divide(listedVehicleCount, listedVehicleSearchDto.PageSize));
        var currentPage = listedVehicleSearchDto.CurrentPage > amountOfPages ?
            amountOfPages : listedVehicleSearchDto.CurrentPage;

        var listedVehiclesPage = listedVehicles.Skip(((int)currentPage - 1) * listedVehicleSearchDto.PageSize).Take(listedVehicleSearchDto.PageSize).ToList();
        var listedVehicleDto = _mapper.Map<IEnumerable<ListedVehicleDto>>(listedVehiclesPage);
        return await CreateResponse(true, listedVehicleCount, 200, "Operation Successful", "no problem!", listedVehicleDto);
    }

    public async Task<Response> MapManySoldListedVehiclesAsync(object responseObject, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var listedVehiclesSold = _mapper.Map<List<ListedVehicleDto>>(responseObject);

        var response = new List<ListedVehicleSoldDto>();

        foreach (var listedVehicle in listedVehiclesSold)
        {
            var intent = await _db.Intents.FirstOrDefaultAsync(i => i.IntentReceiverId == listedVehicle.ApplicationUserId, cancellationToken: cancellationToken);
            var intentDto = _mapper.Map<IntentDto>(intent);
            response.Add(new ListedVehicleSoldDto { Intent = intentDto, ListedVehicle = listedVehicle });
        }

        return await CreateResponse(true, listedVehiclesSold.Count, 200, "Ok", "", response);
    }

    public async Task<Response> MapSingleSoldListedVehicleAsync(object responseObject, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var listedVehiclesSold = _mapper.Map<ListedVehicleDto>(responseObject);
        var intent = await _db.Intents.FirstOrDefaultAsync(i => i.IntentReceiverId == listedVehiclesSold.ApplicationUserId, cancellationToken: cancellationToken);
        var intentDto = _mapper.Map<IntentDto>(intent);

        return await CreateResponse(true, 1, 200, "Ok", "", new ListedVehicleSoldDto { Intent = intentDto, ListedVehicle = listedVehiclesSold });
    }

    public async Task<Response> SetListedVehicleToDeletedAsync(string listedVehicleId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var listedVehicle = await _db.ListedVehicles.FindAsync(listedVehicleId);

        if (listedVehicle == null)
            return await CreateResponse(false, 0, 400, "Could not Delete", $"Could not find the listedVehicle to delete by the Id: {listedVehicleId}", null);

        listedVehicle.IsDeleted = true;
        listedVehicle.DateDeleted = DateTime.Now;

        _db.ChangeTracker.Clear();
        var entity = _db.Update(listedVehicle);
        if (entity.State != EntityState.Modified)
            return await CreateResponse(false, 0, 400, "Error Deleting ", $"The entity state should be EntityState.Deleted but is, {entity.State}", null);
        var result = 0;
        try
        {
            result = await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            return await CreateResponse(false, 0, 400, "Error Deleting the object", "The listed vehicle was found, but could not be deleted", null);
        }
        return await CreateResponse(true, result, 200, "Operation successful", $"The Listed Vehicle was Deleted successfully", null);
    }

    //HELPER METHODS
    private Task<Response> CreateResponse(bool isSuccessful, int count, int statusCode, string title, string message, object? responseObject)
    {
        return Task.FromResult(new Response
        {
            IsSuccessful = isSuccessful,
            StatusCode = statusCode,
            Title = title,
            Message = message,
            TotalResponseCount = count,
            ResponseObject = responseObject
        });
    }
    private async Task<Response> ValidateTransmission(string transmission, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        switch (transmission.ToLower().Trim())
        {
            case "manual":
                return await CreateResponse(true, 1, 200, "Correct", "correct", "Manual");

            case "automatic":
                return await CreateResponse(true, 1, 200, "Correct", "correct", "Automatic");
        }
        return await CreateResponse(false, 0, 409, "Invalid Transmission", "Transmissions can only be, Automatic or Manual", transmission);
    }
    private async Task<Response> ValidateDriveTrain(string driveTrain, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        switch (driveTrain.ToLower().Trim())
        {
            case "rwd":
                return await CreateResponse(true, 1, 200, "Correct", "Correct", "RWD");
            case "fwd":
                return await CreateResponse(true, 1, 200, "Correct", "Correct", "FWD");
            case "awd":
                return await CreateResponse(true, 1, 200, "Correct", "Correct", "AWD");
            case "4x4":
                return await CreateResponse(true, 1, 200, "Correct", "Correct", "4X4");
        }
        return await CreateResponse(false, 0, 409, "Invalid Drive Train", "Drive Trains can only be: RWD, FWD, AWD or 4x4", driveTrain);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ListedVehiclesModels;
using Services.Repository.ListedVehicles;

namespace AutoSellerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ListedVehicleController : ControllerBase
{
    private readonly IListedVehiclesRepository _listedVehiclesRepository;
    public ListedVehicleController(IListedVehiclesRepository listedVehiclesRepository)
    {
        _listedVehiclesRepository = listedVehiclesRepository;
    }

    [HttpGet("GetAllListedVehicles")]
    public async Task<IActionResult> GetAllListedVehicles(CancellationToken cancellationToken)
    {
        var result = await _listedVehiclesRepository.GetAllByAsync(predicate: l => l.IsSold == false && l.IsDeleted == false, orderBy: l => l.DateListed,
            cancellationToken, l => l.Vehicle, l => l.Vehicle.Maker, l => l.ApplicationUser, l => l.Images.OrderBy(i => i.ImageIndex));
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("GetAListedVehiclesByListedVehicleId/{listedVehicleId}")]
    public async Task<IActionResult> GetAListedVehiclesForUserByListedVehicleId(string listedVehicleId,
        CancellationToken cancellationToken)
    {
        var result = await _listedVehiclesRepository.GetSingleByAsync(
            predicate: l => l.IsSold == false && l.ListedVehicleId == listedVehicleId && l.IsDeleted == false, cancellationToken,
             l => l.ApplicationUser, l => l.Vehicle, l => l.Vehicle.Maker, l => l.Images.OrderBy(i => i.ImageIndex));
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("GetAllListedVehiclesBySearch/")]
    public async Task<IActionResult> GetAllListedVehiclesBySearch([FromQuery] ListedVehicleSearchModelDto listedVehicleSearchDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _listedVehiclesRepository.GetAllListedVehiclesBySearchAsync(listedVehicleSearchDto, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("GetAllListedVehiclesWithPages/{pageSize:int}/{currentPage:int}")]
    public async Task<IActionResult> GetAllListedVehiclesWithPages(int pageSize, int currentPage, CancellationToken cancellationToken)
    {
        var result = await _listedVehiclesRepository.GetAllWithPagesAsync(pageSize, currentPage, 
            predicate: l => l.IsSold == false && l.IsDeleted == false, 
            orderBy: l => l.DateListed,
            cancellationToken, l => l.Vehicle, l => l.Vehicle.Maker, l => l.ApplicationUser, l => l.Images.OrderBy(i => i.ImageIndex));
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("GetAllListedVehiclesByModelName/{modelName}")]
    public async Task<IActionResult> GetAllListedVehiclesByModelName(string modelName,
        CancellationToken cancellationToken)
    {
        var result = await _listedVehiclesRepository.GetListedVehicleByVehicleModelNameAsync(modelName, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("GetAllListedVehiclesByModelNameWithPages/{modelName}/{pageSize:int}/{currentPage:int}")]
    public async Task<IActionResult> GetAllListedVehiclesByModelNameWithPages(int pageSize, int currentPage, string modelName,
        CancellationToken cancellationToken)
    {
        var result = await _listedVehiclesRepository.GetAllWithPagesAsync(pageSize, currentPage,
            predicate: l => l.IsSold == false && l.Vehicle.VehicleName == modelName.ToUpper().Trim() && l.IsDeleted == false,
            orderBy: l => l.DateListed,
        cancellationToken,
            l => l.Vehicle, l => l.ApplicationUser, l => l.Images.OrderBy(i => i.ImageIndex));
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpGet("GetAllListedVehiclesForUser/{applicationUserId}")]
    public async Task<IActionResult> GetAllListedVehiclesForUser(string applicationUserId,
        CancellationToken cancellationToken)
    {
        //TODO: Validation that the Token user claimTypes.NameIdentifier == {applicationUserId}
        var result = await _listedVehiclesRepository.GetAllByAsync(
            predicate: l => l.ApplicationUserId == applicationUserId && l.IsSold == false && l.IsDeleted == false,
            orderBy: l => l.DateListed, cancellationToken, l => l.Images.OrderBy(i => i.ImageIndex), l => l.ApplicationUser, l => l.Vehicle, l => l.Vehicle.Maker);
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpGet("GetAllSoldListedVehiclesForUser/{applicationUserId}")]
    public async Task<IActionResult> GetAllSoldListedVehiclesForUser(string applicationUserId,
        CancellationToken cancellationToken)
    {
        //TODO: Validation that the Token user claimTypes.NameIdentifier == {applicationUserId}
        var result = await _listedVehiclesRepository.GetAllByAsync(
            predicate: l => l.ApplicationUserId == applicationUserId && l.IsSold == true && l.IsDeleted == false,
            orderBy: l => l.DateSold, cancellationToken, l => l.Images.OrderBy(i => i.ImageIndex), l => l.ApplicationUser, l => l.Vehicle, l => l.Vehicle.Maker);
        if (result.IsSuccessful)
        {
            var request = await _listedVehiclesRepository.MapManySoldListedVehiclesAsync(result.ResponseObject, cancellationToken);
            return StatusCode(request.StatusCode, request);
        }
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpGet("GetSoldListedVehicleForUser/{listedVehicleId}/{applicationUserId}")]
    public async Task<IActionResult> GetSoldListedVehicleForUser(string listedVehicleId, string applicationUserId,
        CancellationToken cancellationToken)
    {
        var request = await _listedVehiclesRepository.GetSingleByAsync(
            predicate: l => l.IsSold == true && l.ListedVehicleId == listedVehicleId && l.ApplicationUserId == applicationUserId,
            cancellationToken, l=>l.Vehicle.Maker, l => l.Vehicle, l => l.Images.OrderBy(i=>i.ImageIndex), l => l.ApplicationUser);
        return StatusCode(request.StatusCode, request);
    }

    [Authorize]
    [HttpPost("ListAVehicle")]
    public async Task<IActionResult> ListVehicle([FromBody] ListedVehicleCreateDto listedVehicleCreateDto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var validTransmissionAndDriveTrain =
            await _listedVehiclesRepository.ValidateDriveTrainAndTransmissionAsync(listedVehicleCreateDto.DriveTrain,
                listedVehicleCreateDto.Transmission, cancellationToken);

        if (!validTransmissionAndDriveTrain.IsSuccessful)
            return StatusCode(validTransmissionAndDriveTrain.StatusCode, validTransmissionAndDriveTrain);

        var response = await _listedVehiclesRepository.CreateAsync(listedVehicleCreateDto, cancellationToken);
        if (!response.IsSuccessful)
            return StatusCode(response.StatusCode, response);

        return Ok(response);
    }

    [Authorize]
    [HttpPut("UpdateAListedVehicleData")]
    public async Task<IActionResult> UpdateListedVehicleData([FromBody] ListedVehicleUpdateDto listedVehicleUpdateDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var validTransmissionAndDriveTrain =
            await _listedVehiclesRepository.ValidateDriveTrainAndTransmissionAsync(listedVehicleUpdateDto.DriveTrain,
                listedVehicleUpdateDto.Transmission, cancellationToken);

        if (!validTransmissionAndDriveTrain.IsSuccessful)
            return StatusCode(validTransmissionAndDriveTrain.StatusCode, validTransmissionAndDriveTrain);

        var result = await _listedVehiclesRepository.UpdateAsync(listedVehicleUpdateDto, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpPost("SetListedVehicleAsSold/{intentId}/{applicationUserId}")]
    public async Task<IActionResult> ListedVehicleIsSold(string intentId, string applicationUserId, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _listedVehiclesRepository.SetVehicleAsSoldAsync(intentId, applicationUserId, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpDelete("DeleteListedVehicle/{listedVehicleId}")]
    public async Task<IActionResult> DeleteListedVehicle(string listedVehicleId,
        CancellationToken cancellationToken)
    {
        var result = await _listedVehiclesRepository.SetListedVehicleToDeletedAsync(listedVehicleId, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}


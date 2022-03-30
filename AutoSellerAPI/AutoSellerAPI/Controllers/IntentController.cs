using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.IntentsModels;
using Services.Repository.Intents;

namespace AutoSellerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class IntentController : ControllerBase
{
    private readonly IIntentsRepository _intentsRepository;
    public IntentController(IIntentsRepository intentsRepository)
    {
        _intentsRepository = intentsRepository;
    }

    [HttpGet("GetAIntentByIntentId/{intentId}")]
    public async Task<IActionResult> GetAIntentByIntentId(string intentId, CancellationToken cancellationToken)
    {
        var request = await _intentsRepository.GetSingleByAsync(
            predicate: i => i.IntentId == intentId
                           && i.IsDiscarded == false
                           && i.IsSold == false
                           && i.ListedVehicle.IsDeleted == false,
            cancellationToken,
            i => i.ListedVehicle.Vehicle.Maker, i => i.ListedVehicle.Vehicle, i => i.ListedVehicle.Images, i => i.IntentSender, i => i.IntentReceiver);
        return StatusCode(request.StatusCode, request);
    }

    [HttpGet("GetAllIntents")]
    public async Task<IActionResult> GetAllIntents(CancellationToken cancellationToken)
    {
        var result = await _intentsRepository.GetAllByAsync(predicate: i => i.IsSold == false, orderBy: i => i.DateOfIntent,
            cancellationToken, i => i.ListedVehicle);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("GetAllIntentsByPages/{pageSize:int}/{currentPage:int}")]
    public async Task<IActionResult> GetAllIntentsByPages(int pageSize, int currentPage,
        CancellationToken cancellationToken)
    {
        var result = await _intentsRepository.GetAllWithPagesAsync(pageSize, currentPage, predicate: i => i.IsSold == false, orderBy: i => i.DateOfIntent,
            cancellationToken, i => i.ListedVehicle);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("GetIntentsByAppUserId/{senderId}")]
    public async Task<IActionResult> GetIntentsBySenderId(string senderId, CancellationToken cancellationToken)
    {
        var result = await _intentsRepository.GetAllByAsync(
            predicate: p => p.IntentSenderId == senderId && p.IsSold == false && p.IsDiscarded == false
                            && p.ListedVehicle.IsSold == false && p.ListedVehicle.IsDeleted == false,
            orderBy: i => i.DateOfIntent, cancellationToken,
            i => i.ListedVehicle, i => i.ListedVehicle.Vehicle, i => i.ListedVehicle.Vehicle.Maker);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("GetIntentsForAppUserById/{receiverId}")]
    public async Task<IActionResult> GetIntentsForAppUserById(string receiverId, CancellationToken cancellationToken)
    {
        var result = await _intentsRepository.GetAllByAsync(predicate: p => p.IntentReceiverId == receiverId && p.IsSold == false && p.IsDiscarded == false
                                                                            && p.ListedVehicle.IsSold == false && p.ListedVehicle.IsDeleted == false,
            orderBy: i => i.DateOfIntent, cancellationToken,
            i => i.ListedVehicle, i => i.ListedVehicle.Vehicle, i => i.ListedVehicle.Vehicle.Maker, i => i.IntentSender);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("GetIntentsForListedVehicleById/{listedVehicleId}")]
    public async Task<IActionResult> GetIntentsForListedVehicle(string listedVehicleId,
        CancellationToken cancellationToken)
    {
        var result = await _intentsRepository.GetAllByAsync(predicate: i => i.ListedVehicleId == listedVehicleId && i.IsSold == false,
            orderBy: i => i.DateOfIntent, cancellationToken, i => i.ListedVehicle);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("GetIntentsForListedVehicleByModelName/{listedVehicleModelName}")]
    public async Task<IActionResult> GetIntentsForListedVehicleByModelName(string listedVehicleModelName,
        CancellationToken cancellationToken)
    {
        var result = await _intentsRepository.GetIntentsForListedVehicleByModelName(listedVehicleModelName, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("MakeVehicleBuyIntent")]
    public async Task<IActionResult> MakeVehicleBuyIntent([FromBody] IntentCreateDto intentCreateDto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var validIntent = await _intentsRepository.ValidateIntentIsUniqueForUser(intentCreateDto, cancellationToken);
        if (!validIntent.IsSuccessful)
            return StatusCode(validIntent.StatusCode, validIntent);

        var result = await _intentsRepository.CreateAsync(intentCreateDto, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("ToggleIntetIsRead/{intentId}/{applicationUserId}")]
    public async Task<IActionResult> ToggleIntetIsRead(string intentId, string applicationUserId, CancellationToken cancellationToken)
    {
        var request = await _intentsRepository.ToggleIntentIsReadAsync(intentId, applicationUserId, cancellationToken);
        return StatusCode(request.StatusCode, request);
    }

    [HttpPut("SetIntentAsDiscarded/{intentId}/{applicationUserId}")]
    public async Task<IActionResult> MakeIntentAsDiscarded(string intentId, string applicationUserId, CancellationToken cancellationToken)
    {
        var request = await _intentsRepository.SetIntentAsDiscarded(intentId, applicationUserId, cancellationToken);
        return StatusCode(request.StatusCode, request);
    }
}

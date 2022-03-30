using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ResponseModels;
using Models.VehiclesModels;
using Services.Repository.Vehicles;

namespace AutoSellerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class VehicleController : ControllerBase
{
    private readonly IVehicleRepository _vehicleRepository;
    public VehicleController(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    [AllowAnonymous]
    [HttpGet("GetAllVehicleModels")]
    public async Task<IActionResult> GetAllVehicles(CancellationToken cancellationToken)
    {
        var result = await _vehicleRepository.GetAllByAsync(predicate: null, orderBy: v => v.VehicleName, cancellationToken,
            v => v.Maker);

        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpGet("GetAllVehicleModelsWithPages/{pageSize:int}/{currentPage:int}")]
    public async Task<IActionResult> GetAllVehiclesWithPages(int pageSize, int currentPage, CancellationToken cancellationToken)
    {
        var result = await _vehicleRepository.GetAllWithPagesAsync(pageSize, currentPage, predicate: null, orderBy: v => v.VehicleName, cancellationToken,
            v => v.Maker);

        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpGet("GetVehicleModelByName/{vehicleName}")]
    public async Task<IActionResult> GetVehicleByName(string vehicleName, CancellationToken cancellationToken)
    {
        vehicleName = vehicleName.ToUpper().Trim();
        var result = await _vehicleRepository.GetSingleByAsync(predicate: v => v.VehicleName == vehicleName,
                cancellationToken, v => v.Maker);

        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpGet("GetVehicleModelById/{vehicleId}")]
    public async Task<IActionResult> GetVehicleById(string vehicleId, CancellationToken cancellationToken)
    {
        var result = await _vehicleRepository.GetSingleByAsync(predicate: v => v.VehicleId == vehicleId,
                cancellationToken, v => v.Maker);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("CreateVehicleModel")]
    public async Task<IActionResult> CreateVehicle([FromBody] VehicleCreateDto vehicleCreateDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        vehicleCreateDto.VehicleName = vehicleCreateDto.VehicleName.ToUpper().Trim();
        var result = await _vehicleRepository.CreateAsync(vehicleCreateDto, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("CreateManyVehicleModels")]
    public async Task<IActionResult> CreateManyVehicles(
        [FromBody] IEnumerable<VehicleCreateDto> vehicleCreateDtoList, CancellationToken cancellationToken)
    {
        var results = new List<Response>();
        foreach (var vehicle in vehicleCreateDtoList)
        {
            vehicle.VehicleName = vehicle.VehicleName.ToUpper().Trim();
            results.Add(await _vehicleRepository.CreateAsync(vehicle, cancellationToken));
        }
        return Ok(results);
    }

    [HttpPut("UpdateVehicle")]
    public async Task<IActionResult> UpdateVehicle([FromBody] VehicleUpdatetDto vehicleUpdatetDto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _vehicleRepository.UpdateAsync(vehicleUpdatetDto, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("DeleteVehicle/{VehicleId}")]
    public async Task<IActionResult> DeleteVehicle(string vehicleId, CancellationToken cancellationToken)
    {
        var result = await _vehicleRepository.DeleteAsync(vehicleId, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.MakerModels;
using Models.ResponseModels;
using Services.Repository.Makers;
using Services.StaticService;

namespace AutoSellerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = PolicyBasedAuthNames.Developer)]
public class MakerController : ControllerBase
{
    private readonly IMakerRepository _makerRepository;
    public MakerController(IMakerRepository makerRepository)
    {
        _makerRepository = makerRepository;
    }
    
    [AllowAnonymous]
    [HttpGet("GetAllMakers")]
    public async Task<IActionResult> GetAllMakers(CancellationToken cancellationToken)
    {
        var result = await _makerRepository.GetAllByAsync(predicate: null, orderBy: m => m.MakerName, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpGet("GetAllMakersByPages/{pageSize:int}/{currentPage:int}")]
    public async Task<IActionResult> GetAllMakersByPages(int pageSize, int currentPage, CancellationToken cancellationToken)
    {
        var result = await _makerRepository.GetAllWithPagesAsync(pageSize, currentPage, predicate: null, orderBy: m => m.MakerName, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpGet("GetAllVehiclesForMakerByMakerName/{makerName}")]
    public async Task<IActionResult> GetAllVehiclesForMakerByMakerName(string makerName,
        CancellationToken cancellationToken)
    {
        var result = await _makerRepository.GetAllByAsync(predicate: m => m.MakerName == makerName.ToUpper().Trim(), orderBy: m => m.MakerName, cancellationToken, v => v.Vehicles);
        return StatusCode(result.StatusCode, result);
    }

    [AllowAnonymous]
    [HttpGet("GetAllVehiclesForMakerByMakerId/{makerId}")]
    public async Task<IActionResult> GetAllVehiclesForMakerByMakerId(string makerId,
        CancellationToken cancellationToken)
    {
        var result = await _makerRepository.GetAllByAsync(predicate: m => m.MakerId == makerId, orderBy: m => m.MakerName, cancellationToken, v => v.Vehicles);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("CreateMaker")]
    public async Task<IActionResult> CreateMaker([FromBody] MakerCreateDto makerUpsertDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        makerUpsertDto.MakerName = makerUpsertDto.MakerName.ToUpper().Trim();
        var result = await _makerRepository.CreateAsync(makerUpsertDto, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("CreateManyMakers")]
    public async Task<IActionResult> CreateManyMakers([FromBody] IEnumerable<MakerCreateDto> makerUpsertDtoList)
    {
        var results = new List<Response>();
        foreach (var maker in makerUpsertDtoList)
        {
            maker.MakerName = maker.MakerName.ToUpper().Trim();
            results.Add(await _makerRepository.CreateAsync(maker, CancellationToken.None));
        }
        return Ok(results);
    }

    [HttpPut("UpdateMaker")]
    public async Task<IActionResult> UpdateMaker([FromBody] MakerUpdateDto makerUpdateDto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _makerRepository.UpdateAsync(makerUpdateDto, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("DeleteMaker/{MakerId}")]
    public async Task<IActionResult> DeleteMaker(string makerId, CancellationToken cancellationToken)
    {
        var result = await _makerRepository.DeleteAsync(makerId, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}
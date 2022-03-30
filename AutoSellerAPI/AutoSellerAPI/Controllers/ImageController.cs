using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.ImagesModels;
using Models.ResponseModels;
using Services.Repository.Images;

namespace AutoSellerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ImageController : ControllerBase
{
    private readonly IImagesRepository _imgRepository;

    public ImageController(IImagesRepository imagesRepository)
    {
        _imgRepository = imagesRepository;
    }

    [HttpPost("AddImagesToListedVehicle/{listedVehicleId}")]
    public async Task<IActionResult> AddImagesToListedVehicle([FromBody]IEnumerable<ImageCreateDto> imageCreateDtosList, string listedVehicleId,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var request = await _imgRepository.CreateImagesForNewListedVehicleAsync(imageCreateDtosList, listedVehicleId, cancellationToken);
        return Ok(request);
    }

    [HttpPut("UpdateImagesForListedVehicle")]
    public async Task<IActionResult> UpdateImagesForListedVehicle([FromBody]IEnumerable<ImageUpdateDto> imageUpdateDtosList,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var request = await _imgRepository.UpdateImagesAsync(imageUpdateDtosList, cancellationToken);
        return Ok(request);
    }

    [HttpDelete("DeleteImageForListedVehicle/{imageId}")]
    public async Task<IActionResult> DeleteImageForListedVehicle(string imageId,
        CancellationToken cancellationToken)
    {
        var result = await _imgRepository.DeleteAsync(imageId, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}

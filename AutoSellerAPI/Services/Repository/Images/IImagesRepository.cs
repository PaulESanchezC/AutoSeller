using Models.ImagesModels;
using Models.ResponseModels;

namespace Services.Repository.Images;

public interface IImagesRepository : ICrudRepository<Image,ImageDto,ImageCreateDto,ImageUpdateDto>
{
    Task<IEnumerable<Response>> CreateImagesForNewListedVehicleAsync(IEnumerable<ImageCreateDto> imagesCreateDtos, string listedVehicleId,
        CancellationToken cancellationToken);

    Task<Response> UpdateImagesAsync(IEnumerable<ImageUpdateDto> images, CancellationToken cancellationToken);
}
using Models.ImagesModels;

namespace Services.RepositoryServices.ImageRepository;

public interface IImageRepository
{
    Task<IList<ImageUpdateVm>> MoveImageAsync(IList<Images> images, string imageId, int newIndex);
}
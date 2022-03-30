using AutoMapper;
using Models.ImagesModels;

namespace Services.RepositoryServices.ImageRepository;

public class ImageRepository : IImageRepository
{
    private readonly IMapper _mapper;

    public ImageRepository(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<IList<ImageUpdateVm>> MoveImageAsync(IList<Images> images, string imageId, int newIndex)
    {
        var imageToMove = images.FirstOrDefault(i => i.ImageId == imageId);
        var oldIndex = imageToMove.ImageIndex;
        newIndex += imageToMove.ImageIndex;
        imageToMove.ImageIndex = newIndex;
        images[newIndex].ImageIndex = oldIndex;
        

        return Task.FromResult(_mapper.Map<IList<ImageUpdateVm>>(images));
    }
}
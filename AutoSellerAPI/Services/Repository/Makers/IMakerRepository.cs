using Models.MakerModels;

namespace Services.Repository.Makers;

public interface IMakerRepository : ICrudRepository<Maker,MakerDto,MakerCreateDto,MakerUpdateDto>
{
    
}
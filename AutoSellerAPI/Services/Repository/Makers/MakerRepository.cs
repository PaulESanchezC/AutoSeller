using AutoMapper;
using Data;
using Models.MakerModels;

namespace Services.Repository.Makers;

public class MakerRepository : CrudRepository<Maker,MakerDto,MakerCreateDto,MakerUpdateDto> , IMakerRepository
{
    public MakerRepository(ApplicationDbContext db, IMapper mapper) : base(db, mapper)
    {
    }
}
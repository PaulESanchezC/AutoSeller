using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.ImagesModels;
using Models.ResponseModels;

namespace Services.Repository.Images;

public class ImagesRepository : CrudRepository<Image, ImageDto, ImageCreateDto, ImageUpdateDto>, IImagesRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    public ImagesRepository(ApplicationDbContext db, IMapper mapper) : base(db,mapper)
    {
        _db = db;
        _mapper = mapper;
    }


    public async Task<IEnumerable<Response>> CreateImagesForNewListedVehicleAsync(IEnumerable<ImageCreateDto> imagesCreateDtos, string listedVehicleId, CancellationToken cancellationToken)
    {
        var imagesToCreate = _mapper.Map<IList<Image>>(imagesCreateDtos);
        var responseList = new List<Response>();

        foreach (var img in imagesToCreate)
        {
            img.ImageIndex = imagesToCreate.IndexOf(img);
            img.ListedVehicleId = listedVehicleId;

            var entity = await _db.AddAsync(img, cancellationToken);
            if (entity.State != EntityState.Added)
            {
                responseList.Add(await CreateResponse(false,400,"Error Adding Image", "Error adding image, model may be invalid", 1, img));
                continue;
            }

            var result = await _db.SaveChangesAsync(cancellationToken);
            if (result < 1)
            {
                responseList.Add( await CreateResponse(false, 400, "Error Saving Image", "Error saving image, image may be duplicated", 1, img));
                continue;
            }

            var imgDto = _mapper.Map<ImageDto>(img);
            responseList.Add(await CreateResponse(true, 200, "Operation successful", "Operation successful",1, imgDto));
        }
        return responseList;
    }

    public async Task<Response> UpdateImagesAsync(IEnumerable<ImageUpdateDto> images, CancellationToken cancellationToken)
    {
        images = images.OrderBy(i => i.ImageIndex);
        var responseObjects = new List<Response>();
        foreach (var image in images)
        {
            var dbImage = await _db.Images.FindAsync(image.ImageId);
            dbImage.ImageIndex = image.ImageIndex;
            if (dbImage == null)
                responseObjects.Add(await CreateResponse(false, 400, "Invalid image id", "could not find the image", 1, image));

            _db.ChangeTracker.Clear();
            var entity = _db.Update(dbImage);

            if (entity.State != EntityState.Modified)
                responseObjects.Add(await CreateResponse(false, 400, "Error updating the object", $"The entity state should be EntityState.Modified but is, {entity.State}", 1, image));

            try
            {
                await _db.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                responseObjects.Add(await CreateResponse(false, 400, "Error Saving the object", $"Error Saving the object, {entity.State}", 1, image));
            }

            var imageDto = _mapper.Map<ImageDto>(dbImage);
            responseObjects.Add(await CreateResponse(true, 200, "Ok", "Operation Successful ", 1, imageDto));
        }


        return await CreateResponse(true, 200, "Operation Ambiguous, iterate to see each operation's result", "Operation Ambiguous, iterate to see each operation's result", images.Count(), responseObjects);
    }

    // HELPER METHOD
    private Task<Response> CreateResponse(bool isSuccessful, int statusCode, string title, string message, int totalCount ,object? responseObject)
    {
        return Task.FromResult(new Response
        {
            IsSuccessful = isSuccessful,
            StatusCode = statusCode,
            Title = title,
            Message = message,
            TotalResponseCount = totalCount,
            ResponseObject = responseObject
        });
    }


}
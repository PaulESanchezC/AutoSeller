using Models.IntentsModels;
using Models.ResponseModels;

namespace Services.Repository.Intents;

public interface IIntentsRepository : ICrudRepository<Intent,IntentDto,IntentCreateDto,IntentUpdateDto>
{
    Task<Response> ValidateIntentIsUniqueForUser(IntentCreateDto intentCreateDto, CancellationToken cancellationToken);
    Task<Response> GetIntentsForListedVehicleByModelName(string modelName, CancellationToken cancellationToken);
    Task<Response> ToggleIntentIsReadAsync(string intentId, string applicationUserId, CancellationToken cancellationToken);
    Task<Response> SetIntentAsDiscarded(string intentId, string applicationUserId, CancellationToken cancellationToken);
}

using Models.ApplicationUserModels;
using Models.ImagesModels;
using Models.IntentModels;
using Models.ListedVehicleModels;
using Models.PagesViewModels.UserPagesViewModels;
using Models.ResponseModels;

namespace Services.RepositoryServices.UserPagesRepository;

public interface IUserPagesRepository
{
    Task<DashboardVm> GetDashboardListedVehiclesAsync(string userId, string jwtToken);
    Task<List<Intents>> GetReceivedIntentsAsync(string userId, string jwtToken);
    Task<List<Intents>> GetSentIntentsAsync(string userId, string jwtToken);
    Task<Response> RegisterVehicleAsync(ListedVehicleCreateVm listedVehicleCreateVm, string jwtToken);
    Task<IEnumerable<Response>> AddImagesToListedVehicleAsync(string jwtToken, IEnumerable<ImagesCreateVm> imagesCreateVm,string listedVehicleId);
    Task<bool> DeleteImageAsync(string imageId, string jwtToken);
    Task<Response> MakeVehicleBuyIntentAsync(IntentsCreateVm intentsCreateVm, string jwtToken);
    Task<Response> SetVehicleAsSoldAsync(string intentId, string applicationUserId,string jwtToken);
    Task<Response> ToggleIntentIsReadAsync(string intentId, string applicationUserId, string jwtToken);
    Task<Response> SetIntentAsDiscardedAsync(string intentId, string applicationUserId, string jwtToken);

    //Helper Methods
    Task<List<Response>> MoveImagesAsync(IList<ImageUpdateVm> imageUpdate, string jwtToken);
    Task<ListedVehicle> MapListedVehicleAsync(object listedVehicleObject);
    Task<ApplicationUser> MapApplicationUserLoginResponseAsync(object loginResponseObject);
    Task<ListedVehicleUpdateVm> MapListedVehicleUpdateAsync(ListedVehicle listedVehicle);
    Task<ListedVehicleCreateVm> MapListedVehicleCreateVmAsync(RegisterAVehicleVm listedVehicleObject);
    Task<Images> MapImagesAsync(object imageObject);
    Task<Intents> MapIntentAsync(object intentObject);
}
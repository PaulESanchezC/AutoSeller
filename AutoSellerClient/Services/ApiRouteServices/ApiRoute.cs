using Microsoft.Extensions.Options;
using Models.ApiRouteOptionsModels;

namespace Services.ApiRouteServices;

public class ApiRoute : IApiRoute
{
    private readonly ApiRouteOptions _apiRoute;

    public ApiRoute(IOptions<ApiRouteOptions> routeOptions)
    {
        _apiRoute = routeOptions.Value;
    }

    public string GetAllListedVehiclesWithPages() => $"{_apiRoute.BaseUrl}api/ListedVehicle/GetAllListedVehiclesWithPages/";
    public string GetAllListedVehiclesBySearch()  => $"{_apiRoute.BaseUrl}api/ListedVehicle/GetAllListedVehiclesBySearch";
    public string GetAListedVehiclesByListedVehicleId() => $"{_apiRoute.BaseUrl}api/ListedVehicle/GetAListedVehiclesByListedVehicleId/";
    public string RegisterUser() => $"{_apiRoute.BaseUrl}api/Account/Register/";
    public string LoginUser() => $"{_apiRoute.BaseUrl}api/Account/Login";
    public string GetEmailConfirmationToken() => $"{_apiRoute.BaseUrl}api/Account/GetEmailConfirmationTokenAsync/";
    public string ValidateEmailConfirmationToken() => $"{_apiRoute.BaseUrl}api/Account/EmailConfirmation/";

    public string LoginWithFacebook() => $"{_apiRoute.BaseUrl}api/Account/LoginWithFacebook/{{0}}";

    public string GetPasswordToken() => $"{_apiRoute.BaseUrl}api/Account/GenerateResetPasswordToken/";
    public string ValidatePasswordToken() => $"{_apiRoute.BaseUrl}api/Account/ValidateResetPasswordToken/";
    public string GetAllMakers() => $"{_apiRoute.BaseUrl}api/Maker/GetAllMakers/";
    public string GetAllVehicleModels() => $"{_apiRoute.BaseUrl}api/Vehicle/GetAllVehicleModels";
    public string GetAllVehiclesForMakerByMakerId() => $"{_apiRoute.BaseUrl}api/Maker/GetAllVehiclesForMakerByMakerId/";
    
    //Users
    public string UserGetAllListedVehicles() => $"{_apiRoute.BaseUrl}api/ListedVehicle/GetAllListedVehiclesForUser/";
    public string UserGetAllSoldListedVehicles() => $"{_apiRoute.BaseUrl}api/ListedVehicle/GetAllSoldListedVehiclesForUser/";
    public string UserGetIntentsSent() => $"{_apiRoute.BaseUrl}api/Intent/GetIntentsByAppUserId/";
    public string UserGetIntentsReceived() => $"{_apiRoute.BaseUrl}api/Intent/GetIntentsForAppUserById/";
    public string UserRegisterVehicle() => $"{_apiRoute.BaseUrl}api/ListedVehicle/ListAVehicle";
    public string UserAddImagesToListedVehicle() => $"{_apiRoute.BaseUrl}api/Image/AddImagesToListedVehicle/";
    public string UserUpdateListedVehicle() => $"{_apiRoute.BaseUrl}api/ListedVehicle/UpdateAListedVehicleData";
    public string UserDeleteListedVehicle() => $"{_apiRoute.BaseUrl}api/ListedVehicle/DeleteListedVehicle/";
    public string UserDeleteImage() => $"{_apiRoute.BaseUrl}api/Image/DeleteImageForListedVehicle/";
    public string UserUpdateImage() => $"{_apiRoute.BaseUrl}api/Image/UpdateImagesForListedVehicle";
    public string UserSetUserPassword() => $"{_apiRoute.BaseUrl}api/Account/SetPasswordAsync/";
    public string UserUpdateUserProfile() => $"{_apiRoute.BaseUrl}api/Account/UpdateApplicationUserProfile";
    public string UserResetPassword() => $"{_apiRoute.BaseUrl}api/Account/ResetPasswordAsync";
    public string UserMakeVehicleBuyIntent() => $"{_apiRoute.BaseUrl}api/Intent/MakeVehicleBuyIntent";
    public string UserGetAIntentByIntentId() => $"{_apiRoute.BaseUrl}api/Intent/GetAIntentByIntentId/";
    public string UserSetListedVehicleAsSold() => $"{_apiRoute.BaseUrl}api/ListedVehicle/SetListedVehicleAsSold/";
    public string UserSetIntentAsDiscarded() => $"{_apiRoute.BaseUrl}api/Intent/SetIntentAsDiscarded/";
    //TODO: Fix typos in api routes => Fix Typo in unit Test
    public string UserToggleIntentIsRead() => $"{_apiRoute.BaseUrl}api/Intent/ToggleIntetIsRead/";
    public string UserGetSoldListedVehicle() => $"{_apiRoute.BaseUrl}api/ListedVehicle/GetSoldListedVehicleForUser/{{0}}/{{1}}";

}
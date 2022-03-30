namespace Services.ApiRouteServices;

public interface IApiRoute
{
    string GetAllListedVehiclesWithPages();
    string GetAllListedVehiclesBySearch();
    string GetAListedVehiclesByListedVehicleId();
    string RegisterUser();
    string LoginUser();
    string GetEmailConfirmationToken();
    string ValidateEmailConfirmationToken();
    string LoginWithFacebook();
    string GetPasswordToken();
    string ValidatePasswordToken();
    string GetAllMakers();
    string GetAllVehicleModels();
    string GetAllVehiclesForMakerByMakerId();

    //User

    string UserGetAllListedVehicles();
    string UserGetAllSoldListedVehicles();
    string UserGetIntentsSent();
    string UserGetIntentsReceived();
    string UserRegisterVehicle();
    string UserAddImagesToListedVehicle();
    string UserUpdateListedVehicle();
    string UserDeleteListedVehicle();
    string UserDeleteImage();
    string UserUpdateImage();
    string UserSetUserPassword();
    string UserUpdateUserProfile();
    string UserResetPassword();
    string UserMakeVehicleBuyIntent();
    string UserGetAIntentByIntentId();
    string UserSetListedVehicleAsSold();
    string UserSetIntentAsDiscarded();
    string UserToggleIntentIsRead();
    string UserGetSoldListedVehicle();

}
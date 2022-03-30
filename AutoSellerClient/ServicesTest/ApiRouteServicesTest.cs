using Microsoft.Extensions.Options;
using Models.ApiRouteOptionsModels;
using Moq;
using NUnit.Framework;
using Services.ApiRouteServices;

namespace ServicesTest;
[TestFixture]
public class ApiRouteServicesTest
{
    //TestClass
    private ApiRoute ApiRoute;

    //Dependencies
    private Mock<ApiRouteOptions> _apiRouteMock;
    private Mock<IOptions<ApiRouteOptions>> _routeOptionsMock;

    [SetUp]
    public void Setup()
    {
        _apiRouteMock = new Mock<ApiRouteOptions>();
        _routeOptionsMock = new Mock<IOptions<ApiRouteOptions>>();
        _apiRouteMock.Object.BaseUrl = "baseUrl/";
        _routeOptionsMock.Setup(option => option.Value).Returns(_apiRouteMock.Object);

        ApiRoute = new ApiRoute(_routeOptionsMock.Object);
    }

    [Test]
    public void ApiRoute_VerifyIOptionsBaseUrl()
    {
        //Arrange
        //Setup

        //Act
        var result = _routeOptionsMock.Object.Value.BaseUrl;
        //Assert
        Assert.NotNull(result);
        Assert.AreEqual(result, "baseUrl/");
    }

    #region RouteReturnTests
    [Test]
    public void GetAllListedVehiclesWithPages_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/ListedVehicle/GetAllListedVehiclesWithPages/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.GetAllListedVehiclesWithPages();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void GetAllListedVehiclesBySearch_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/ListedVehicle/GetAllListedVehiclesBySearch";
        var expectedResult = $"baseUrl/{route}";


        //Act
        var result = ApiRoute.GetAllListedVehiclesBySearch();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }


    [Test]
    public void GetAListedVehiclesByListedVehicleId_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/ListedVehicle/GetAListedVehiclesByListedVehicleId/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.GetAListedVehiclesByListedVehicleId();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void RegisterUser_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Account/Register/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.RegisterUser();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void LoginUser_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Account/Login";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.LoginUser();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void GetEmailConfirmationToken_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Account/GetEmailConfirmationTokenAsync/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.GetEmailConfirmationToken();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void ValidateEmailConfirmationToken_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Account/EmailConfirmation/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.ValidateEmailConfirmationToken();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void LoginWithFacebook_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Account/LoginWithFacebook/{0}";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.LoginWithFacebook();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void GetPasswordToken_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Account/GenerateResetPasswordToken/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.GetPasswordToken();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void ValidatePasswordToken_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Account/ValidateResetPasswordToken/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.ValidatePasswordToken();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void GetAllMakers_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Maker/GetAllMakers/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.GetAllMakers();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void GetAllVehicleModels_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Vehicle/GetAllVehicleModels";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.GetAllVehicleModels();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void GetAllVehiclesForMakerByMakerId_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Maker/GetAllVehiclesForMakerByMakerId/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.GetAllVehiclesForMakerByMakerId();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserGetAllListedVehicles_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/ListedVehicle/GetAllListedVehiclesForUser/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserGetAllListedVehicles();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserGetAllSoldListedVehicles_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/ListedVehicle/GetAllSoldListedVehiclesForUser/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserGetAllSoldListedVehicles();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserGetIntentsSent_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Intent/GetIntentsByAppUserId/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserGetIntentsSent();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserGetIntentsReceived_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Intent/GetIntentsForAppUserById/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserGetIntentsReceived();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserRegisterVehicle_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/ListedVehicle/ListAVehicle";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserRegisterVehicle();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserAddImagesToListedVehicle_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Image/AddImagesToListedVehicle/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserAddImagesToListedVehicle();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserUpdateListedVehicle_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/ListedVehicle/UpdateAListedVehicleData";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserUpdateListedVehicle();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserDeleteListedVehicle_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/ListedVehicle/DeleteListedVehicle/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserDeleteListedVehicle();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserDeleteImage_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Image/DeleteImageForListedVehicle/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserDeleteImage();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserUpdateImage_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Image/UpdateImagesForListedVehicle";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserUpdateImage();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserSetUserPassword_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Account/SetPasswordAsync/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserSetUserPassword();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserUpdateUserProfile_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Account/UpdateApplicationUserProfile";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserUpdateUserProfile();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserResetPassword_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Account/ResetPasswordAsync";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserResetPassword();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserMakeVehicleBuyIntent_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Intent/MakeVehicleBuyIntent";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserMakeVehicleBuyIntent();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserGetAIntentByIntentId_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Intent/GetAIntentByIntentId/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserGetAIntentByIntentId();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserSetListedVehicleAsSold_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/ListedVehicle/SetListedVehicleAsSold/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserSetListedVehicleAsSold();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserSetIntentAsDiscarded_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Intent/SetIntentAsDiscarded/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserSetIntentAsDiscarded();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserToggleIntentIsRead_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/Intent/ToggleIntetIsRead/";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserToggleIntentIsRead();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    [Test]
    public void UserGetSoldListedVehicle_VerifyRouteReturn()
    {
        //Arrange
        var route = "api/ListedVehicle/GetSoldListedVehicleForUser/{0}/{1}";
        var expectedResult = $"baseUrl/{route}";

        //Act
        var result = ApiRoute.UserGetSoldListedVehicle();

        Assert.NotNull(result);
        Assert.AreEqual(expectedResult, result);
    }

    #endregion
}
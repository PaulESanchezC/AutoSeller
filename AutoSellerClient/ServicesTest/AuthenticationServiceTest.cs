using System.Net;
using System.Text;
using AutoMapper;
using Models.ApplicationUserModels;
using Models.AuthenticationModels;
using Models.ResponseModels;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using Services.ApiRouteServices;
using Services.AuthenticationService;
using static System.Net.Http.HttpMethod;

namespace ServicesTest;

[TestFixture]
public class AuthenticationServiceTest
{
    //Test Class
    private AuthenticationServices authenticationServices;

    //Dependencies
    private Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private Mock<IHttpClientFactory> _httpClientFactoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<IApiRoute> _routeMock;
    private Mock<IAuthenticationServices> _authenticationServicesMock;

    [SetUp]
    public void Setup()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _mapperMock = new Mock<IMapper>();
        _routeMock = new Mock<IApiRoute>();
        _authenticationServicesMock = new Mock<IAuthenticationServices>();
        authenticationServices =
            new AuthenticationServices(_httpClientFactoryMock.Object, _mapperMock.Object, _routeMock.Object);
    }
    
    [Test]
    public async Task LoginAsync_Verify_PostMethod_ApiRoute_ResponseType()
    {
        //Arrange
        var loginVm = new ApplicationUserLoginVm
        {
            AccessToken = "none",
            ExternalLogin = "local",
            Password = "password",
            Username = "email@test.com"
        };
        var expectedContentResponse = new ApplicationUserLoginResponse
        {
            ApplicationUserDto = new ApplicationUser(),
            JWToken = "token",
            LoginSuccessful = true,
            Message = "message"
        };
        _routeMock.Setup(x => x.LoginUser()).Returns("https://baseUrl/api/Account/Login");
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedContentResponse), Encoding.UTF8, "application/json")
            }).Verifiable();

        _httpClientFactoryMock.Setup(c => c.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));

        //Act
        var result = await authenticationServices.LoginAsync(loginVm);

        //Assert
        Assert.NotNull(result);
        Assert.AreEqual(expectedContentResponse.GetType(),result.GetType());
        Assert.AreEqual(typeof(ApplicationUserLoginResponse), result.GetType());
        _httpMessageHandlerMock.Protected().Verify("SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(method => method.Method == Post && method.RequestUri.AbsolutePath == "/api/Account/Login"),
            ItExpr.IsAny<CancellationToken>());
    }

    [Test]
    public async Task LoginWithFacebookAsync_Verify_PostMethod_ApiRoute_ResponseType()
    {
        //Arrange
        var accessToken = "accessToken";
        var facebookRegistrationVm = new FacebookRegistrationVm
        {
            FirstName = "First name",
            LastName = "Last name",
            Address = "Address",
            City = "City",
            PhoneNumber = "phone number",
            StateOrProvince = "State or Province",
        };
        var expectedContentResponse = new FacebookLoginResponse
        {
            AccessToken = "access token",
            IsAccessTokenValid = true,
            IsRegistrationSuccessful = false,
            IsLoginSuccessful = true,
            StatusCode = 200,
            Message = "message",
            UserRegistrationAttempt = new ApplicationUserRegistrationResponse(),
            UserLoginAttempt = new ApplicationUserLoginResponse()
        };

        _routeMock.Setup(r => r.LoginWithFacebook()).Returns("https://baseUrl/api/Account/LoginWithFacebook/accessToken");
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedContentResponse), Encoding.UTF8, "application/json")
            }).Verifiable();

        _httpClientFactoryMock.Setup(c => c.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));

        //Act
        var result = await authenticationServices.LoginWithFacebookAsync(accessToken, facebookRegistrationVm);

        //Assert
        Assert.NotNull(result);
        Assert.AreEqual(expectedContentResponse.GetType(), result.GetType());
        _httpMessageHandlerMock.Protected().Verify("SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(method => method.Method == Post && method.RequestUri.AbsolutePath == "/api/Account/LoginWithFacebook/accessToken"),
            ItExpr.IsAny<CancellationToken>());
    }

    [Test]
    public async Task RegisterUserAsync_VerifyRegistrationNotSuccessful_Response()
    {
        //Arrange
        var appUserRegistrationVm = new ApplicationUserRegisterVm
        {
            Username = "email@test.com",
            Password = "password",
            ConfirmPaswword = "password",
            FirstName = "First name",
            LastName = "Last name",
            PhoneNumber = "phone number",
            Address = "Address",
            City = "City",
            StateOrProvince = "State or Province",
            ExternalLogin = "local",
            AccessToken = ""
        };
        var expectedContentResponse = new Response()
        {
            IsSuccessful = false,
            StatusCode = 409,
            Title = "Registration Failed",
            Message = "Response Message",
            TotalResponseCount = 0,
            ResponseObject = new ApplicationUser
            {
                Token = string.Empty,
            }
        };
        _routeMock.Setup(r => r.RegisterUser()).Returns("https://baseurl/api/Account/Register/");
        
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Conflict,
                Content = new StringContent(JsonConvert.SerializeObject(expectedContentResponse),Encoding.UTF8,"application/json")
            }).Verifiable();

        _httpClientFactoryMock.Setup(c => c.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));

        //Act
        var result = await authenticationServices.RegisterUserAsync(appUserRegistrationVm);

        //Assert
        Assert.NotNull(result);
        Assert.AreEqual(expectedContentResponse.GetType(),result.GetType());
        Assert.AreEqual(409,result.StatusCode);
        Assert.AreEqual("Response Message", result.Message);
        Assert.AreEqual("Registration Failed", result.Title);
        Assert.AreEqual(0, result.TotalResponseCount);
        _httpMessageHandlerMock.Protected().Verify("SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == Post && req.RequestUri.AbsolutePath == "/api/Account/Register/"),
            ItExpr.IsAny<CancellationToken>()
            );
    }

    [Test]
    public async Task RegisterUserAsync_Verify_RegistrationSuccessful_ResponseObjectNotNull()
    {
        //Arrange
        var appUserRegistrationVm = new ApplicationUserRegisterVm
        {
            Username = "email@test.com",
            Password = "password",
            ConfirmPaswword = "password",
            FirstName = "First name",
            LastName = "Last name",
            PhoneNumber = "phone number",
            Address = "Address",
            City = "City",
            StateOrProvince = "State or Province",
            ExternalLogin = "local",
            AccessToken = "none"
        };
        var responseAppUser = new ApplicationUser
        {
            Email = "email@test.com",
            FirstName = "First name",
            LastName = "Last name",
            PhoneNumber = "phone number",
            Address = "Address",
            City = "City",
            StateOrProvince = "State or Province",
            Token = "email confirmation token",
            Id = "Id",
            HasPassword = true,
            Roles = new List<string>()
        };

        _authenticationServicesMock.Setup(method => method.RegisterUserAsync(It.IsAny<ApplicationUserRegisterVm>()))
            .ReturnsAsync(new Response
            {
                IsSuccessful = true,
                StatusCode = 201,
                Title = "Registration Successful",
                Message = "message",
                TotalResponseCount = 1,
                ResponseObject = responseAppUser
            }).Verifiable();

        //Act
        var result = await _authenticationServicesMock.Object.RegisterUserAsync(appUserRegistrationVm);

        //Assert
        Assert.NotNull(result);
        Assert.AreEqual(typeof(Response),result.GetType());
        Assert.NotNull(result.ResponseObject);
    }

    [Test]
    public async Task GetEmailConfirmationToken_Verify_ResponseReturn()
    {
        //Arrange
        var responseContent = new Response
        {
            ResponseObject = "email confirmation token",
            IsSuccessful = true,
            Message = "message",
            StatusCode = 200,
            Title = "title",
            TotalResponseCount = 1
        };
        _routeMock.Setup(r => r.GetEmailConfirmationToken()).Returns("https://baseurl/api/Account/GetEmailConfirmationTokenAsync/");
        _httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                Content = new StringContent(JsonConvert.SerializeObject(responseContent), Encoding.UTF8, "application/json")
            }).Verifiable();
        
        _httpClientFactoryMock.Setup(c => c.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));

        //Act
        var result = await authenticationServices.GetEmailConfirmationToken("email@test.com");

        //Assert
        Assert.NotNull(result);
        Assert.AreEqual(result.GetType(),responseContent.GetType());
        Assert.AreEqual(result.ResponseObject,responseContent.ResponseObject);
    }

    [Test]
    public async Task ValidateEmailConfirmationToken_Verify_ResponseNotNull_ResponseType()
    {
        //Arrange
        _authenticationServicesMock.Setup(method =>
                method.ValidateEmailConfirmationToken(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new Response()).Verifiable();

        //Act
        var result = await _authenticationServicesMock.Object.ValidateEmailConfirmationToken("email@test.com",
            "email confirmation token");

        //Assert
        Assert.NotNull(result);
        Assert.AreEqual(typeof(Response),result.GetType());
    }
}
using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Models.ApplicationUserModels;
using Models.AuthenticationModels;
using Models.PagesViewModels;
using Models.ResponseModels;
using Newtonsoft.Json;
using Services.ApiRouteServices;
using Services.JwtBearerTokens;

namespace Services.AuthenticationService;

public class AuthenticationServices : IAuthenticationServices
{
    private readonly IApiRoute _route;
    private readonly IHttpClientFactory _client;
    private readonly IMapper _mapper;

    public AuthenticationServices(IHttpClientFactory client, IMapper mapper, IApiRoute route)
    {
        _client = client;
        _mapper = mapper;
        _route = route;
    }

    public async Task<ApplicationUserLoginResponse> LoginAsync(ApplicationUserLoginVm applicationUserLoginVm)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _route.LoginUser());
        request.Content = new StringContent(JsonConvert.SerializeObject(applicationUserLoginVm), Encoding.UTF8,
            "application/Json");

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<ApplicationUserLoginResponse>(responseString);
        return response;
    }
    public async Task<FacebookLoginResponse> LoginWithFacebookAsync(string accessToken, FacebookRegistrationVm? facebookRegistrationVm)
    {
        var route = string.Format(_route.LoginWithFacebook(), accessToken);
        var request = new HttpRequestMessage(HttpMethod.Post, route);

        request.Content = new StringContent(JsonConvert.SerializeObject(facebookRegistrationVm), Encoding.UTF8, "application/json");

        var client = await _client.CreateClient().SendAsync(request);

        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<FacebookLoginResponse>(responseString);
        return response;
    }
    public async Task<Response> RegisterUserAsync(ApplicationUserRegisterVm applicationUserRegisterVm)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _route.RegisterUser());
        request.Content = new StringContent(JsonConvert.SerializeObject(applicationUserRegisterVm), Encoding.UTF8,
            "application/json");
        var client = await _client.CreateClient().SendAsync(request);

        var jsonStringResponse = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<ApplicationUserRegistrationResponse>(jsonStringResponse);
        if (!response.RegistrationSuccessful)
            return await CreateResponseAsync(false, "Registration Failed", response.Message, 409, 0, response.ApplicationUserDto);

        var user = _mapper.Map<ApplicationUser>(response.ApplicationUserDto);

        var emailConfirmationToken = await GetEmailConfirmationToken(user.Email);
        if (!emailConfirmationToken!.IsSuccessful)
            return emailConfirmationToken;

        user.Token = (string)emailConfirmationToken.ResponseObject!;
        return await CreateResponseAsync(true, "Registration Successful", response.Message, 201, 1, user);
    }
    public async Task<Response> GetEmailConfirmationToken(string userEmail)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _route.GetEmailConfirmationToken() + userEmail);
        var client = await _client.CreateClient().SendAsync(request);
        var response = JsonConvert.DeserializeObject<Response>(await client.Content.ReadAsStringAsync());
        return response!;
    }
    public async Task<Response> ValidateEmailConfirmationToken(string email, string emailConfirmationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _route.ValidateEmailConfirmationToken());
        var emailConfirmation = new EmailConfirmation
        {
            Email = email,
            Token = emailConfirmationToken
        };
        request.Content = new StringContent(JsonConvert.SerializeObject(emailConfirmation), Encoding.UTF8, "application/json");

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);

        return response;
    }
    public async Task<Response> SetLoginPasswordAsync(SetLoginPasswordVm setOAuthUserPasswordVm, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _route.UserSetUserPassword());
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);
        request.Content = new StringContent(JsonConvert.SerializeObject(setOAuthUserPasswordVm), Encoding.UTF8, "application/json");

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response!;
    }
    public async Task<Response> GetResetPasswordTokenAsync(string userEmail)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _route.GetPasswordToken() + userEmail);

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response;
    }
    public async Task<Response> ValidateResetPasswordTokenAsync(string userEmail, string resetPasswordToken)
    {
        ResetPasswordValidatorVm tokenValidator = new() { Email = userEmail, ResetPasswordToken = resetPasswordToken };
        var request = new HttpRequestMessage(HttpMethod.Post, _route.ValidatePasswordToken());
        request.Content = new StringContent(JsonConvert.SerializeObject(tokenValidator), Encoding.UTF8, "application/json");

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response;
    }
    public async Task<Response> ResetPasswordAsync(ResetPasswordVm resetPasswordVm)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _route.UserResetPassword());
        request.Content = new StringContent(JsonConvert.SerializeObject(resetPasswordVm), Encoding.UTF8, "application/json");

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response>(responseString);
        return response;
    }
    public async Task<ApplicationUserLoginResponse> UpdateUserProfileAsync(ApplicationUser applicationUser, string jwtToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, _route.UserUpdateUserProfile());
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken);
        request.Content =
            new StringContent(JsonConvert.SerializeObject(applicationUser), Encoding.UTF8, "application/json");

        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<ApplicationUserLoginResponse>(responseString);
        return response;
    }

    //Helper Methods
    private Task<Response> CreateResponseAsync(bool isSuccessful, string title, string message, int statusCode, int totalResponseCount, object responseObject)
    {
        return Task.FromResult(
            new Response
            {
                IsSuccessful = isSuccessful,
                Title = title,
                Message = message,
                StatusCode = statusCode,
                TotalResponseCount = totalResponseCount,
                ResponseObject = responseObject
            });
    }


}
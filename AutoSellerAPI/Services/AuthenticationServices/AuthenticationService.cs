using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Models.ApplicationUsersModels;
using Models.AppSettingsOptionsModels;
using Models.AuthenticationModels;
using Models.ExternalLoginModels;
using Models.ResponseModels;
using Newtonsoft.Json;
using Services.JwtServices;
using Services.StaticService;

namespace Services.AuthenticationServices;

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<ApplicationUser> _singInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtServices _jwtServices;
    private readonly IMapper _mapper;
    private readonly FacebookOptions _facebook;
    private readonly IHttpClientFactory _client;

    public AuthenticationService(
        SignInManager<ApplicationUser> singInManager,
        UserManager<ApplicationUser> userManager,
        IJwtServices jwtServices,
        IMapper mapper,
        IOptions<FacebookOptions> options, IHttpClientFactory client)
    {
        _singInManager = singInManager;
        _userManager = userManager;
        _jwtServices = jwtServices;
        _mapper = mapper;
        _client = client;
        _facebook = options.Value;
    }

    public async Task<UserLoginAttemptDto> LoginAsync(UserLoginDto userLoginDto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var login = await _singInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, true);
        if (!login.Succeeded)
            return await CreateLoginAttemptDtoResponseAsync(false, null, "", $"Failed to login, the {login}");

        var user = await _userManager.FindByEmailAsync(userLoginDto.Username);

        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        if (!isEmailConfirmed)
            return await CreateLoginAttemptDtoResponseAsync(false, null, "", "Failed to login, the user has not confirmed the account");

        var userDto = _mapper.Map<ApplicationUserDto>(user);
        userDto.Roles = (await GetUserRolesAsync(user)).ToList();
        userDto.HasPassword = await _userManager.HasPasswordAsync(user);
        var token = await _jwtServices.GenerateTokenAsync(user, userDto.HasPassword);

        return await CreateLoginAttemptDtoResponseAsync(true, userDto, token, "Successfully logged in");
    }
    public async Task<UserRegistrationAttemptDto> RegisterAsync(UserRegistrationDto userRegistrationDto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<ApplicationUser>(userRegistrationDto);
        user.Email = user.UserName;

        var result = await _userManager.CreateAsync(user, userRegistrationDto.Password);
        if (!result.Succeeded)
            return await CreateUserRegistrationAttemptDtoResponseAsync(false, null, "Could not Register user");

        user = await _userManager.FindByEmailAsync(user.Email);
        await _userManager.AddToRoleAsync(user, RoleTypes.Guest);

        var userDto = _mapper.Map<ApplicationUserDto>(user);
        userDto.Roles.Add(RoleTypes.Guest);
        userDto.HasPassword = true;
        return await CreateUserRegistrationAttemptDtoResponseAsync(true, userDto, "Successfully Registered the user");
    }
    public async Task<Response> GetEmailConfirmationTokenAsync(string userEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            return await CreateResponseAsync(false, 400, "Invalid User Id", "Unable to find user", userEmail);
        
        var isAccountActive = await _userManager.IsEmailConfirmedAsync(user);
        if (isAccountActive)
            return await CreateResponseAsync(false, 409, "Active Account",
                "Your account is confirmed and active, there is no need to send any account confirmation emails for this account, Thank you for using AutoSeller",
                userEmail);

        var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return await CreateResponseAsync(true, 200, "Operation successful", "Email sent", emailConfirmationToken);
    }
    public async Task<Response> ValidateEmailConfirmationForUserAsync(string userEmail, string emailConfirmationToken,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
            return await CreateResponseAsync(false, 400, "Invalid User", "Unable to find user", userEmail);


        var confirmation = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);
        if (confirmation.Succeeded)
            return await CreateResponseAsync(true, 200, "Email Confirmed", "Email confirmation has be proved successful, Thank you!", userEmail);
        return await CreateResponseAsync(false, 400, "Invalid Token", "Unable to Validate Token", userEmail);
    }
    public async Task<Response> GenerateResetPasswordTokenAsync(string userEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user == null)
            return await CreateResponseAsync(false, 300, "Ambiguous Operation", "Ambiguous Operation", userEmail);

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (!hasPassword)
            return await CreateResponseAsync(false, 300, "Ambiguous Operation", "Ambiguous Operation", userEmail);

        var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        return await CreateResponseAsync(true, 200, "Ok", "Operation successful", passwordResetToken);
    }

    public async Task<Response> ValidateResetPasswordTokenAsync(string userEmail, string resetPasswordToken, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user == null)
            return await CreateResponseAsync(false, 300, "Ambiguous Operation", "Ambiguous Operation", null);

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (!hasPassword)
            return await CreateResponseAsync(false, 300, "Ambiguous Operation", "Ambiguous Operation", userEmail);

        var result = await _userManager.VerifyUserTokenAsync(
            user,
            _userManager.Options.Tokens.PasswordResetTokenProvider, 
            "ResetPassword",
            resetPasswordToken);
        
        if (result)
        {
            return await CreateResponseAsync(true, 200, "Ok", "Token is valid", null);
        }
        return await CreateResponseAsync(false, 300, "Ambiguous Operation", "Ambiguous Operation", null);
    }

    public async Task<Response> ResetPasswordAsync(ResetPasswordDto resetPasswordDto,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.UserEmail);

        if (user == null)
            return await CreateResponseAsync(false, 300, "Ambiguous Operation", "Ambiguous Operation", null);

        var result = await _userManager.VerifyUserTokenAsync(user,
            _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword",
            resetPasswordDto.ResetPasswordToken);
        
        if (result)
        {
            var request = await _userManager.ResetPasswordAsync(user, resetPasswordDto.ResetPasswordToken, resetPasswordDto.Password);
            return await CreateResponseAsync(true, 200, "Password is Reset", "Password is reset successfully", null);
        }
        return await CreateResponseAsync(false, 300, "Ambiguous Operation", "Ambiguous Operation", null);
    }
    public async Task<Response> FacebookTokenValidatorAsync(string accessToken)
    {
        var tokenDebugRoute = string.Format(FacebookOAuth.TokenDebug, accessToken, _facebook.AppId, _facebook.AppSecret);
        var request = new HttpRequestMessage(HttpMethod.Get, tokenDebugRoute);
        var client = await _client.CreateClient().SendAsync(request);

        var response = await client.Content.ReadAsStringAsync();
        var tokenDebugResponse = JsonConvert.DeserializeObject<FacebookTokenValidationResult>(response);
        var statusCode = tokenDebugResponse.Data.IsValid ? 200 : 498;

        return await CreateResponseAsync(tokenDebugResponse!.Data.IsValid, statusCode, $"Facebook Invalid Token Code:{tokenDebugResponse.Data.Error.Code}", tokenDebugResponse.Data.Error.Message, tokenDebugResponse);
    }
    public async Task<FacebookLoginResultDto> LoginWithFacebookAsync(string accessToken, FacebookRegistrationDto? facebookRegistrationDto)
    {
        var validateToken = await FacebookTokenValidatorAsync(accessToken);
        if (!validateToken.IsSuccessful)
            return await CreateFacebookLoginResultDtoResponse(false, 407, false, false, validateToken.Title, validateToken.Message, null, null);

        var route = string.Format(FacebookOAuth.UserInfo, accessToken);
        var request = new HttpRequestMessage(HttpMethod.Get, route);
        var client = await _client.CreateClient().SendAsync(request);
        var responseString = await client.Content.ReadAsStringAsync();
        var facebookUserInfo = JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseString);

        //Login
        var user = await _userManager.FindByEmailAsync(facebookUserInfo.Username);
        if (user != null)
        {
            var userDto = _mapper.Map<ApplicationUserDto>(user);
            var userEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            if (!userEmailConfirmed)
            {
                var loginAttemptFail = await CreateLoginAttemptDtoResponseAsync(false, userDto, "", "Login Failed, user email is not yet confirmed");
                var userRegistered = await CreateUserRegistrationAttemptDtoResponseAsync(true, userDto,
                    "Login Failed, user email is not yet confirmed");
                return await CreateFacebookLoginResultDtoResponse(false, 401, false, true, accessToken, "Login Failed", loginAttemptFail, userRegistered);
            }

            userDto.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            userDto.HasPassword = await _userManager.HasPasswordAsync(user);
            var jwtToken = await _jwtServices.GenerateTokenAsync(user, userDto.HasPassword);
            var loginAttempSuccess = await CreateLoginAttemptDtoResponseAsync(true, userDto, jwtToken, "Login successful");
            return await CreateFacebookLoginResultDtoResponse(true, 200, isLoginSuccessful: true, false, accessToken, "Login successful", loginAttempSuccess, null);
        }

        //Register
        if (facebookRegistrationDto.StateOrProvince != "abstract")
        {
            try
            {
                var facebookUser = new ApplicationUser
                {
                    FirstName = facebookRegistrationDto.FirstName,
                    LastName = facebookRegistrationDto.LastName,
                    PhoneNumber = facebookRegistrationDto.PhoneNumber,
                    Address = facebookRegistrationDto.Address,
                    City = facebookRegistrationDto.City,
                    StateOrProvince = facebookRegistrationDto.StateOrProvince,
                    Email = facebookUserInfo.Username,
                    UserName = facebookUserInfo.Username
                };
                var createdResult = await _userManager.CreateAsync(facebookUser);
                if (createdResult.Succeeded)
                {
                    var createdUser = await _userManager.FindByEmailAsync(facebookUser.Email);
                    await _userManager.AddToRoleAsync(createdUser, RoleTypes.Guest);
                    var createdUserDto = _mapper.Map<ApplicationUserDto>(createdUser);
                    createdUserDto.Roles.Add(RoleTypes.Guest);
                    var registrationResponseDto = await CreateUserRegistrationAttemptDtoResponseAsync(true, createdUserDto,
                        "User registered successfully, please confirm email to log in");

                    return await CreateFacebookLoginResultDtoResponse(true, 200, false, isRegistrationSuccessful: true, accessToken, "Registered successfully", null,
                        registrationResponseDto);
                }
            }
            catch (Exception e)
            {
                return await CreateFacebookLoginResultDtoResponse(false, 409, false, false, "Error occurred", e.Message, null, null);
            }
        }

        //Request missing information to successfully register
        var incompleteUser = new ApplicationUserDto
        {
            FirstName = facebookUserInfo.FirstName,
            LastName = facebookUserInfo.LastName,
            Email = facebookUserInfo.Username,
            PhoneNumber = "",
            Address = "",
            City = "",
            StateOrProvince = "",
        };
        var registrationInfoRequest = await CreateUserRegistrationAttemptDtoResponseAsync(false, incompleteUser,
            "Please provide this missing information to successfully register the user");
        return await CreateFacebookLoginResultDtoResponse(true, 469, false, false, accessToken, registrationInfoRequest.Message, null,
            registrationInfoRequest);
    }
    public async Task<Response> SetPasswordAsync(SetUserPasswordDto setUserPasswordDto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _userManager.FindByEmailAsync(setUserPasswordDto.ApplicationUserEmail);
        if (user is null)
        {
            setUserPasswordDto.CurrentPassword = "";
            setUserPasswordDto.NewPassword = "";
            return await CreateResponseAsync(false, 400, "Invalid user", "Invalid user email",
                setUserPasswordDto);
        }

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (!hasPassword)
        {
            var addPassword = await _userManager.AddPasswordAsync(user, setUserPasswordDto.NewPassword);
            if (addPassword.Succeeded)
            {
                setUserPasswordDto.CurrentPassword = "";
                setUserPasswordDto.NewPassword = "";
                return await CreateResponseAsync(true, 200, "Ok", "You have successfully added a password", setUserPasswordDto);
            }
        }

        var setPassword = await _userManager.ChangePasswordAsync(user, setUserPasswordDto.CurrentPassword,
            setUserPasswordDto.NewPassword);
        if (setPassword.Succeeded)
        {
            setUserPasswordDto.CurrentPassword = "";
            setUserPasswordDto.NewPassword = "";
            return await CreateResponseAsync(true, 200, "Ok", "You have successfully modified your password", setUserPasswordDto);
        }

        return await CreateResponseAsync(false, 400, "Operation failed", "Operation failed, an unexpected error has occurred", setUserPasswordDto);
    }
    public async Task<UserLoginAttemptDto> UpdateApplicationUserProfileAsync(ApplicationUserDto applicationUserDto, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var user = await _userManager.FindByEmailAsync(applicationUserDto.Email);

        if (user.Id != applicationUserDto.Id)
            return await CreateLoginAttemptDtoResponseAsync(false, null, "", "Invalid User Id");

        user.FirstName = applicationUserDto.FirstName;
        user.LastName = applicationUserDto.LastName;
        user.PhoneNumber = applicationUserDto.PhoneNumber;
        user.Address = applicationUserDto.Address;
        user.City = applicationUserDto.City;
        user.StateOrProvince = applicationUserDto.StateOrProvince;
        var updateUser = await _userManager.UpdateAsync(user);

        if (updateUser.Succeeded)
        {
            var newUser = await _userManager.FindByEmailAsync(user.Email);
            var userDto = _mapper.Map<ApplicationUserDto>(newUser);
            userDto.Roles = (await GetUserRolesAsync(user)).ToList();
            userDto.HasPassword = await _userManager.HasPasswordAsync(user);
            var jwtToken = await _jwtServices.GenerateTokenAsync(newUser, userDto.HasPassword);
            return await CreateLoginAttemptDtoResponseAsync(true, userDto, jwtToken, "Invalid User Id");
        }
        return await CreateLoginAttemptDtoResponseAsync(false, null, "", "Operation Failed");
    }


    //PRIVATE HELPER METHODS
    private Task<UserLoginAttemptDto> CreateLoginAttemptDtoResponseAsync(bool loginSuccessful,
        ApplicationUserDto? applicationUserDto, string jwToken, string message)
    {
        return Task.FromResult(new UserLoginAttemptDto
        {
            LoginSuccessful = loginSuccessful,
            ApplicationUserDto = applicationUserDto,
            JWToken = jwToken,
            Message = message
        });
    }
    private Task<UserRegistrationAttemptDto> CreateUserRegistrationAttemptDtoResponseAsync(bool registrationSuccessful,
        ApplicationUserDto? applicationUserDto, string message)
    {
        return Task.FromResult(new UserRegistrationAttemptDto
        {
            ApplicationUserDto = applicationUserDto,
            RegistrationSuccessful = registrationSuccessful,
            Message = message
        });
    }
    private async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }
    private Task<Response> CreateResponseAsync(bool isSuccessful, int statusCode, string title, string message, object? responseObject)
    {
        return Task.FromResult(new Response
        {
            IsSuccessful = isSuccessful,
            StatusCode = statusCode,
            Title = title,
            Message = message,
            ResponseObject = responseObject
        });
    }
    private Task<FacebookLoginResultDto> CreateFacebookLoginResultDtoResponse(bool isAccessTokenValid, int statusCode, bool isLoginSuccessful,
        bool isRegistrationSuccessful, string accessToken, string message,
        UserLoginAttemptDto? loginAttemptDto, UserRegistrationAttemptDto? registrationAttemptDto)
    {
        return Task.FromResult(
            new FacebookLoginResultDto
            {
                IsAccessTokenValid = isAccessTokenValid,
                StatusCode = statusCode,
                IsLoginSuccessful = isLoginSuccessful,
                IsRegistrationSuccessful = isRegistrationSuccessful,
                AccessToken = accessToken,
                Message = message,
                UserLoginAttempt = loginAttemptDto,
                UserRegistrationAttempt = registrationAttemptDto
            });
    }
}
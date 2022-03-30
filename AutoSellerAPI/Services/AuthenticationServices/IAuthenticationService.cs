using Microsoft.AspNetCore.Authentication;
using Models.ApplicationUsersModels;
using Models.AuthenticationModels;
using Models.ResponseModels;

namespace Services.AuthenticationServices;

public interface IAuthenticationService
{
    Task<UserLoginAttemptDto> LoginAsync(UserLoginDto userLoginDto, CancellationToken cancellationToken);
    Task<UserRegistrationAttemptDto> RegisterAsync(UserRegistrationDto userRegistrationDto, CancellationToken cancellationToken);
    Task<Response> GetEmailConfirmationTokenAsync(string userEmail, CancellationToken cancellationToken);
    Task<Response> ValidateEmailConfirmationForUserAsync(string userEmail, string emailConfirmationToken,
        CancellationToken cancellationToken);
    Task<Response> GenerateResetPasswordTokenAsync(string userEmail, CancellationToken cancellationToken);
    Task<Response> ValidateResetPasswordTokenAsync(string userEmail,string resetPasswordToken, CancellationToken cancellationToken);
    Task<Response> ResetPasswordAsync(ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken);

    Task<Response> FacebookTokenValidatorAsync(string accessToken);
    Task<FacebookLoginResultDto> LoginWithFacebookAsync(string accessToken, FacebookRegistrationDto? facebookRegistrationDto);

    Task<Response> SetPasswordAsync(SetUserPasswordDto setUserPasswordDto,
        CancellationToken cancellationToken);
    Task<UserLoginAttemptDto> UpdateApplicationUserProfileAsync(ApplicationUserDto applicationUserDto, CancellationToken cancellationToken);

}
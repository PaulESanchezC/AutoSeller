using Models.ApplicationUserModels;
using Models.AuthenticationModels;
using Models.PagesViewModels;
using Models.ResponseModels;

namespace Services.AuthenticationService;

public interface IAuthenticationServices
{
    Task<ApplicationUserLoginResponse> LoginAsync(ApplicationUserLoginVm applicationUserLoginVm);
    Task<FacebookLoginResponse> LoginWithFacebookAsync(string accessToken, FacebookRegistrationVm? facebookRegistrationVm);
    Task<Response> RegisterUserAsync(ApplicationUserRegisterVm applicationUserRegisterVm);
    Task<Response> GetEmailConfirmationToken(string userEmail);
    Task<Response> ValidateEmailConfirmationToken(string email, string emailConfirmationToken);
    Task<Response> SetLoginPasswordAsync(SetLoginPasswordVm setOAuthUserPasswordVm, string jwtToken);
    Task<Response> GetResetPasswordTokenAsync(string userEmail);
    Task<Response> ValidateResetPasswordTokenAsync(string userEmail, string resetPasswordToken);
    Task<Response> ResetPasswordAsync(ResetPasswordVm resetPasswordVm);
    Task<ApplicationUserLoginResponse> UpdateUserProfileAsync(ApplicationUser applicationUser, string jwtToken);
}
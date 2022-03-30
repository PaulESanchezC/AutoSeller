using Models.ApplicationUsersModels;

namespace Models.AuthenticationModels;

public class FacebookLoginResultDto
{
    public int StatusCode { get; set; }
    public bool IsAccessTokenValid { get; set; }
    public string AccessToken { get; set; }
    public bool IsLoginSuccessful { get; set; }
    public bool IsRegistrationSuccessful { get; set; }
    public string Message { get; set; }
    public UserLoginAttemptDto? UserLoginAttempt { get; set; }
    public UserRegistrationAttemptDto? UserRegistrationAttempt { get; set; }
}
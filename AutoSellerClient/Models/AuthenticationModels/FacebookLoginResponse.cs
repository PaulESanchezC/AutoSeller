namespace Models.AuthenticationModels;

public class FacebookLoginResponse
{
    public int StatusCode { get; set; }
    public bool IsAccessTokenValid { get; set; }
    public string AccessToken { get; set; }
    public bool IsLoginSuccessful { get; set; }
    public bool IsRegistrationSuccessful { get; set; }
    public string Message { get; set; }
    public ApplicationUserLoginResponse? UserLoginAttempt { get; set; }
    public ApplicationUserRegistrationResponse? UserRegistrationAttempt { get; set; }
}
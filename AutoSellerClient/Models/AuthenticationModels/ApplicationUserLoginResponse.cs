using Models.ApplicationUserModels;

namespace Models.AuthenticationModels;

public class ApplicationUserLoginResponse
{
    public bool LoginSuccessful { get; set; }
    public string JWToken { get; set; }
    public ApplicationUser? ApplicationUserDto { get; set; }
    public string Message { get; set; }
}
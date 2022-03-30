using Models.ApplicationUsersModels;

namespace Models.AuthenticationModels;

public class UserLoginAttemptDto
{
    public bool LoginSuccessful { get; set; }
    public string JWToken { get; set; }
    public ApplicationUserDto? ApplicationUserDto { get; set; }
    public string Message { get; set; }
}
using Models.ApplicationUsersModels;

namespace Models.AuthenticationModels;

public class UserRegistrationAttemptDto
{
    public bool RegistrationSuccessful { get; set; }
    public ApplicationUserDto? ApplicationUserDto { get; set; }
    public string Message { get; set; }
}
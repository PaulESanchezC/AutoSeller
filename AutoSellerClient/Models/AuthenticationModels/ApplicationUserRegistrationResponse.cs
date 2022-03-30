using Models.ApplicationUserModels;

namespace Models.AuthenticationModels;

public class ApplicationUserRegistrationResponse
{
    public bool RegistrationSuccessful { get; set; }
    public ApplicationUser ApplicationUserDto { get; set; }
    public string Message { get; set; }
}
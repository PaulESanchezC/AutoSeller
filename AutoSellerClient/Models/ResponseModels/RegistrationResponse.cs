using Models.ApplicationUserModels;

namespace Models.ResponseModels;

public class RegistrationResponse
{
    public bool RegistrationSuccessful { get; set; }
    public string Message { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
}
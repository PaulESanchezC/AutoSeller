
using Models.AuthenticationModels;

namespace Services.AuthenticationService.Templates;

public class FacebookTemplates
{
    public static FacebookRegistrationVm RegistrationVm => new () 
    {
        FirstName = "abstract",
        LastName = "abstract",
        PhoneNumber = "abstract",
        Address = "abstract",
        City = "abstract",
        StateOrProvince = "abstract"
    };
}


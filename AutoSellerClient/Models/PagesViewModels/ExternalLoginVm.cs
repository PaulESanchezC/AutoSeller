using Models.AuthenticationModels;

namespace Models.PagesViewModels;

public class ExternalLoginVm
{
    public string LoginProvider { get; set; }
    public string AccessToken { get; set; }
    public FacebookRegistrationVm FacebookRegistrationVm { get; set; }
    public string? ReturnUrl { get; set; } = null;
}
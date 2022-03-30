using Models.ApplicationUserModels;

namespace Models.PagesViewModels;

public class RegisterPageVm
{
    public ApplicationUserRegisterVm ApplicationUserRegisterVm { get; set; }
    public List<string> AuthenticationProviders { get; set; }
}
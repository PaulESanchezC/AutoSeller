using Models.ApplicationUserModels;
using Models.AuthenticationModels;

namespace Models.PagesViewModels.UserPagesViewModels;

public class ProfileVm
{
    public ApplicationUser ApplicationUser { get; set; } = new ApplicationUser();
    public SetLoginPasswordVm SetLoginPasswordVm { get; set; } = new SetLoginPasswordVm();
    public ResetPasswordVm ResetPasswordVm { get; set; } = new ResetPasswordVm();
}
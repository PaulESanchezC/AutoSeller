using Models.ApplicationUserModels;

namespace Models.PagesViewModels;

public class EmailConfirmationPageVm
{
    public string Email { get; set; }
    public bool IsUserConfirmed { get; set; }
}
namespace Services.TutorialServices;

public interface ITutorialService
{
    Task<bool> BlockUserCrudAsync(string userId, string userEmail);
    Task<bool> BlockUserCrudAsync(string userEmail);
}
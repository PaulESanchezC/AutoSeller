namespace Services.TutorialServices;

public class TutorialService : ITutorialService
{
    public Task<bool> BlockUserCrudAsync(string userId, string userEmail)
    {
        var id = userId is "eff2f35a-488b-49da-84d4-b396b60d2ade" or "0ec1aac5-4c9e-4213-b573-9984c16d8ec1" or "dce92991-de0e-4390-b83b-87f8d7047f50";

        var normalizedEmail = userEmail.ToLower().Trim();
        var email = normalizedEmail is "user1@tutorial.com" or "user2@tutorial.com" or "user3@tutorial.com";

        return Task.FromResult(id || email);
    }

    public Task<bool> BlockUserCrudAsync(string userEmail)
    {
        var normalizedEmail = userEmail.ToLower().Trim();
        return Task.FromResult(normalizedEmail is "user1@tutorial.com" or "user2@tutorial.com" or "user3@tutorial.com");
    }
}
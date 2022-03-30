using Models.ApplicationUsersModels;

namespace Services.JwtServices;

public interface IJwtServices
{
    Task<string> GenerateTokenAsync(ApplicationUser appUser, bool hasPassword);
}
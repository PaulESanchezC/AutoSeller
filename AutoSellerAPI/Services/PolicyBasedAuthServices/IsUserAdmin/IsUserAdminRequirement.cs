using Microsoft.AspNetCore.Authorization;

namespace Services.PolicyBasedAuthServices.IsUserAdmin;

public class IsUserAdminRequirement : IAuthorizationRequirement
{
    public string Code { get; set; }
    public string Name { get; set; }
    public IsUserAdminRequirement(string code, string name)
    {
        Code = code;
        Name = name;
    }
}
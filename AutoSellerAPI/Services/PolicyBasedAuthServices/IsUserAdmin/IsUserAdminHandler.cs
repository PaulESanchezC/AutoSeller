using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Services.StaticService;

namespace Services.PolicyBasedAuthServices.IsUserAdmin;

public class IsUserAdminHandler : AuthorizationHandler<IsUserAdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsUserAdminRequirement requirement)
    {
        if (context.User.IsInRole(RoleTypes.Admin))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var code = context.User.FindFirst(ClaimTypes.Hash).Value;
        var name = context.User.FindFirst(ClaimTypes.GivenName).Value;
        if (code == requirement.Code && name == requirement.Name)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
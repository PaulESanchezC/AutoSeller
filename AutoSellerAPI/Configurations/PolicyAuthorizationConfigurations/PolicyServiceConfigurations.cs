using Configurations.ConfigurationsHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.PolicyAuthorizationConfigurationsModels;
using Services.PolicyBasedAuthServices.IsUserAdmin;
using Services.StaticService;

namespace Configurations.PolicyAuthorizationConfigurations;

public static class PolicyServiceConfigurations
{
    public static IServiceCollection AddPolicyServiceConfiguration(this IServiceCollection services)
    {
        var code = ProxyConfiguration.Use.GetSection("PolicyBasedAuth").GetSection("AdminRequirements").GetValue<string>("Code");
        var name = ProxyConfiguration.Use.GetSection("PolicyBasedAuth").GetSection("AdminRequirements").GetValue<string>("Name");

        services.Configure<PolicyServiceOptions>(ProxyConfiguration.Use.GetSection("PolicyBasedAuth").GetSection("AdminRequirements"));

        services.AddScoped<IAuthorizationHandler, IsUserAdminHandler>();
        services.AddAuthorization(p =>
        {
            p.AddPolicy(PolicyBasedAuthNames.Developer, policy => policy.Requirements.Add(new IsUserAdminRequirement(code,name)));
        });
        return services;
    }
}
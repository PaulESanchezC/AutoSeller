using Microsoft.Extensions.DependencyInjection;
using Services.AuthenticationServices;

namespace Configurations.ServicesConfigurations;

public static class AuthenticationServiceConfiguration
{
    public static IServiceCollection AddAuthenticationServiceConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}
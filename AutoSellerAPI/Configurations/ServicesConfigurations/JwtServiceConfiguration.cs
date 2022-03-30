using Microsoft.Extensions.DependencyInjection;
using Services.JwtServices;

namespace Configurations.ServicesConfigurations;

public static class JwtServiceConfiguration
{
    public static IServiceCollection AddJwtServiceConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IJwtServices, JwtServices>();
        return services;
    }
}
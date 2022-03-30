using Microsoft.Extensions.DependencyInjection;

namespace Configurations.CorsConfigurations;

public static class CorsConfiguration
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(opt => opt.AddPolicy("AllowAnonymous", build =>
            build.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
        return services;
    }
}
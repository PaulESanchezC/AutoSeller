using Microsoft.Extensions.DependencyInjection;

namespace Configurations.AutoMapperConfigurations;
public static class AutoMapperServiceConfiguration
{
    public static IServiceCollection AddAutoMapperMapConfigurations(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperOptionsConfigurations));
        return services;
    }
}
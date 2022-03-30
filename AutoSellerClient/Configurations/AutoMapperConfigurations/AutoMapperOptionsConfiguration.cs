using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Configurations.AutoMapperConfigurations;

public static class AutoMapperOptionsConfiguration
{
    public static IServiceCollection AddAutoMapperConfigurations(this IServiceCollection services)
    {
        services.AddAutoMapper(options =>
        {
            options.AllowNullCollections = true;
        });
        return services;
    }
}
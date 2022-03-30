using Configurations.ConfigurationProxyHelper;
using Microsoft.Extensions.DependencyInjection;
using Models.ApiRouteOptionsModels;

namespace Configurations.ApiRouteOptionsConfigurations;

public static class ApiRouteOptionsConfiguration
{
    public static IServiceCollection AddApiRouteOptionsConfiguration(this IServiceCollection services)
    {
        services.Configure<ApiRouteOptions>(ProxyConfiguration.Use.GetSection("ApiRoute"));
        return services;
    }
}
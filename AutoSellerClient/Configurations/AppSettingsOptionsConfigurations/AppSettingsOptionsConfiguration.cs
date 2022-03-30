using Configurations.ConfigurationProxyHelper;
using Microsoft.Extensions.DependencyInjection;
using Models.AppSettingsOptionsModels;

namespace Configurations.AppSettingsOptionsConfigurations;

public static class AppSettingsOptionsConfiguration
{
    public static IServiceCollection AddAppSettingsOptionsConfigurations(this IServiceCollection services)
    {
        services.Configure<MailjetOptions>(ProxyConfiguration.Use.GetSection("MailJet"));
        return services;
    }
}
using Configurations.ConfigurationsHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.AppSettingsOptionsModels;

namespace Configurations.ExternalLoginConfiguration.FacebookLoginConfiguration;

public static class FacebookLoginConfiguration
{
    public static IServiceCollection AddFacebookLoginConfiguration(this IServiceCollection services)
    {
        services.Configure<FacebookOptions>(ProxyConfiguration.Use.GetSection("OAuthLogins:Facebook"));
        services.AddAuthentication().AddFacebook(options =>
        {
            options.AppId = ProxyConfiguration.Use.GetSection("OAuthLogins").GetSection("Facebook").GetValue<string>("AppId");
            options.AppSecret = ProxyConfiguration.Use.GetSection("OAuthLogins").GetSection("Facebook").GetValue<string>("AppSecret");
        });
        return services;
    }
}
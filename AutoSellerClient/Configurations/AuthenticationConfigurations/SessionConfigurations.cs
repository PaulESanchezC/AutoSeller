using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Services.JwtBearerTokens;

namespace Configurations.AuthenticationConfigurations;

public static class SessionConfigurations
{
    public static IServiceCollection AddSessionConfiguration(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.Name = JwtBearerDefaults.SessionCookie;
        });
        return services;
    }
}
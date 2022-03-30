using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Services.JwtBearerTokens;
using Services.Routes;

namespace Configurations.AuthenticationConfigurations;

public static class HttpCookieConfigurations
{
    public static IServiceCollection AddHttpCookiesConfiguration(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.LoginPath = LocalRoute.LoginPath;
                options.AccessDeniedPath = LocalRoute.LoginPath;
                options.LogoutPath = LocalRoute.LogoutPath;
                options.Cookie.Name = JwtBearerDefaults.Cookie;
                options.SlidingExpiration = true;
            });
        return services;
    }
}
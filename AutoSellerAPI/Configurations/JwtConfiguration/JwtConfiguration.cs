using System.Text;
using Configurations.ConfigurationsHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models.JwtServiceConfigurationModel;

namespace Configurations.JwtConfiguration;

public static class JwtConfiguration
{
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services)
    {
        services.Configure<JwtServiceConfiguration>(ProxyConfiguration.Use.GetSection("JwtOptions"));
        var jwtSecretKey = ProxyConfiguration.Use.GetValue<string>("JwtOptions:SecretKey");
        var key = Encoding.ASCII.GetBytes(jwtSecretKey);

        services.AddAuthentication(schema =>
        {
            schema.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            schema.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            schema.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearer =>
        {
            bearer.RequireHttpsMetadata = true;
            bearer.SaveToken = true;
            bearer.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateActor = true,

                RequireAudience = true,
                ValidateAudience = true,
                ValidAudience = ProxyConfiguration.Use.GetValue<string>("JwtOptions:ValidAudience"),
                ValidateIssuer = true,
                ValidIssuer = ProxyConfiguration.Use.GetValue<string>("JwtOptions:ValidIssuer"),

                ClockSkew = TimeSpan.Zero
            };
        });
        return services;
    }

}
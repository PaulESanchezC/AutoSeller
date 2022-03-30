using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Configurations.SwaggerGenConfigurations;

public static class SwaggerGenConfiguration
{
    public static IServiceCollection AddSwaggerGenSecuritySchemeConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            { 
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Provide token in the next field to Authenticate",
                Type = SecuritySchemeType.ApiKey
            });
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
        });
        return services;
    }
}
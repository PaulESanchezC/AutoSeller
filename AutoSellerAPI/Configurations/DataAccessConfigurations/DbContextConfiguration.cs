using Configurations.ConfigurationsHelper;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Configurations.DataAccessConfigurations;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContextOptions(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(ProxyConfiguration.Use.GetConnectionString("DevelopmentConnection"));
        });
        return services;
    }
}
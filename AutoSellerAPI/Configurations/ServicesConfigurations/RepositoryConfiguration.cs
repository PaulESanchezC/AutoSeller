using Microsoft.Extensions.DependencyInjection;
using Services.Repository.Images;
using Services.Repository.Intents;
using Services.Repository.ListedVehicles;
using Services.Repository.Makers;
using Services.Repository.Vehicles;

namespace Configurations.ServicesConfigurations;

public static class RepositoryConfiguration
{
    public static IServiceCollection AddRepositoryServiceConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IMakerRepository, MakerRepository>();
        services.AddScoped<IIntentsRepository, IntentRepository>();
        services.AddScoped<IListedVehiclesRepository, ListedVehiclesRepository>();
        services.AddScoped<IImagesRepository, ImagesRepository>();

        return services;
    }
}
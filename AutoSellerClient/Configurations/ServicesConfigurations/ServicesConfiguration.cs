using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Services.ApiRouteServices;
using Services.AuthenticationService;
using Services.EmailServices;
using Services.IndexPageVnService;
using Services.RepositoryServices.AplicationUsesrRepository;
using Services.RepositoryServices.ImageRepository;
using Services.RepositoryServices.IntentsRepository;
using Services.RepositoryServices.ListedVehiclesRepository;
using Services.RepositoryServices.MakersRepository;
using Services.RepositoryServices.UserPagesRepository;
using Services.RepositoryServices.VehiclesRepository;
using Services.TutorialServices;

namespace Configurations.ServicesConfigurations;

public static class ServicesConfiguration
{
    public static IServiceCollection AddRepositoryServicesConfigurations(this IServiceCollection services)
    {
        //Repository service
        services.AddScoped<IMakersRepository, MakersRepository>();
        services.AddScoped<IVehiclesRepository, VehicleRepository>();
        services.AddScoped<IIndexPageVmService, IndexPageVmService>();
        services.AddScoped<IListedVehicleRepository, ListedVehicleRepository>();
        services.AddScoped<IIntentRepository, IntentsRepository>();
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IUserPagesRepository, UserPagesRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();

        //Authentication Services
        services.AddScoped<IAuthenticationServices, AuthenticationServices>();
        //EmailSender Services
        services.AddTransient<IMailJetEmailSender, EmailSender>();
        //Identity Services
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //Api Route Services
        services.AddScoped<IApiRoute, ApiRoute>();

        //Tutorial Services
        services.AddScoped<ITutorialService, TutorialService>();
        return services;
    }
}
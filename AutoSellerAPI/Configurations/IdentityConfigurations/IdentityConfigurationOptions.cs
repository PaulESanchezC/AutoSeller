using Configurations.ConfigurationsHelper;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.ApplicationUsersModels;

namespace Configurations.IdentityConfigurations;

public static class IdentityConfigurationOptions
{
    public static IServiceCollection AddIdentityConfigurationOptions(this IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password = PasswordOptionsConfigurations(options.Password);
                options.Lockout = LockoutOptionsConfigurations(options.Lockout);
                options.Tokens = TokenOptionsConfigurations(options.Tokens);
                options.SignIn = SignInOptionsConfigurations(options.SignIn);
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }
    public static PasswordOptions PasswordOptionsConfigurations(this PasswordOptions options)
    {
        options.RequireUppercase = ProxyConfiguration.Use.GetValue<bool>("PasswordOptions:RequireUppercase"); 
        options.RequireLowercase = ProxyConfiguration.Use.GetValue<bool>("PasswordOptions:RequireLowercase"); 
        options.RequireDigit = ProxyConfiguration.Use.GetValue<bool>("PasswordOptions:RequireDigit"); 
        options.RequireNonAlphanumeric = ProxyConfiguration.Use.GetValue<bool>("PasswordOptions:RequireNonAlphanumeric");

        options.RequiredLength = ProxyConfiguration.Use.GetValue<int>("PasswordOptions:RequiredLength");
        options.RequiredUniqueChars = ProxyConfiguration.Use.GetValue<int>("PasswordOptions:RequiredUniqueChars");
        return options;
    }
    public static TokenOptions TokenOptionsConfigurations(this TokenOptions options)
    {
        options.AuthenticatorIssuer = ProxyConfiguration.Use.GetValue<string>("TokenOptions:AuthenticatorIssuer");
        return options;
    }
    public static LockoutOptions LockoutOptionsConfigurations(this LockoutOptions options)
    {
        options.AllowedForNewUsers = ProxyConfiguration.Use.GetValue<bool>("LockoutOptions:AllowedForNewUsers");
        options.DefaultLockoutTimeSpan = ProxyConfiguration.Use.GetValue<TimeSpan>("LockoutOptions:DefaultLockoutTimeSpan");
        options.MaxFailedAccessAttempts = ProxyConfiguration.Use.GetValue<int>("LockoutOptions:MaxFailedAccessAttempts"); ;
        return options;
    }
    public static SignInOptions SignInOptionsConfigurations(this SignInOptions builder)
    {
        builder.RequireConfirmedAccount = ProxyConfiguration.Use.GetValue<bool>("SignInOptions:RequireConfirmedAccount");
        builder.RequireConfirmedEmail = ProxyConfiguration.Use.GetValue<bool>("SignInOptions:RequireConfirmedEmail");
        builder.RequireConfirmedPhoneNumber = ProxyConfiguration.Use.GetValue<bool>("SignInOptions:RequireConfirmedPhoneNumber");
        return builder;
    }
}
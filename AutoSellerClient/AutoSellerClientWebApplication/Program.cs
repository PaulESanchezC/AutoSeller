using Configurations.ApiRouteOptionsConfigurations;
using Configurations.AppSettingsOptionsConfigurations;
using Configurations.AuthenticationConfigurations;
using Configurations.AutoMapperConfigurations;
using Configurations.ConfigurationProxyHelper;
using Configurations.JsonConfigurations;
using Configurations.ServicesConfigurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
// Configuration Proxy Helper
ProxyConfiguration.Initialize(builder.Configuration);
// AppSettings Options Configurations
builder.Services.AddAppSettingsOptionsConfigurations();
// AutoMapper Services Configurations
builder.Services.AddAutoMapper(typeof(AddAutoMapperMapConfigurations));
builder.Services.AddAutoMapperConfigurations();
// Services Configurations
builder.Services.AddRepositoryServicesConfigurations();
// Json Convert Options Configurations
builder.Services.AddJsonConfigurations();
// Api Route options Configurations
builder.Services.AddApiRouteOptionsConfiguration();

// Authentication Configurations
// Cookie Authentication
builder.Services.AddHttpCookiesConfiguration();
// Session Cookie
builder.Services.AddSessionConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseCors(c => c
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

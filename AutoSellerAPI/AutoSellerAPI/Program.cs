using Configurations.AutoMapperConfigurations;
using Configurations.ConfigurationsHelper;
using Configurations.CorsConfigurations;
using Configurations.DataAccessConfigurations;
using Configurations.ExternalLoginConfiguration.FacebookLoginConfiguration;
using Configurations.IdentityConfigurations;
using Configurations.JsonConfigurations;
using Configurations.JwtConfiguration;
using Configurations.PolicyAuthorizationConfigurations;
using Configurations.ServicesConfigurations;
using Configurations.SwaggerGenConfigurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
// Proxy Configuration helper
ProxyConfiguration.Initialize(builder.Configuration);
// DataAccess Configuration
builder.Services.AddDbContextOptions();
// Json Configurations
builder.Services.AddJsonConfigurations();
// Identity Configurations
builder.Services.AddIdentityConfigurationOptions();
// Auto Mapper Service Configuration
builder.Services.AddAutoMapperMapConfigurations();
// Repository Service Configuration
builder.Services.AddRepositoryServiceConfiguration();
// Authentication Service Configuration
builder.Services.AddAuthenticationServiceConfiguration();
// Jwt Service Configuration
builder.Services.AddJwtServiceConfiguration();
// SwaggerGen Bearer Token Service Configuration
builder.Services.AddSwaggerGenSecuritySchemeConfiguration();
//Cors Service Configuration
builder.Services.AddCorsConfiguration();

// Authentication Configurations
// Jwt Authentication Configurations
builder.Services.AddJwtConfiguration();
// External Login Configurations
builder.Services.AddFacebookLoginConfiguration();

// Authorization Configurations
// Policy Based Authorization Configuration
builder.Services.AddPolicyServiceConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI(sw =>
{
    sw.RoutePrefix = string.Empty;
    sw.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoSeller");
});

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

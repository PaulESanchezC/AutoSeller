namespace Models.JwtServiceConfigurationModel;

public class JwtServiceConfiguration
{
    public string SecretKey { get; set; }
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
}
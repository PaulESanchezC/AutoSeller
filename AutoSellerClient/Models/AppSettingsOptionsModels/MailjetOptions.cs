namespace Models.AppSettingsOptionsModels;

public class MailjetOptions
{
    public string ApiKey { get; set; }
    public string SecretKey { get; set; }
    public string FromEmail { get; set; }
    public string FromName { get; set; }
}
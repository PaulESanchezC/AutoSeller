namespace Models.AuthenticationModels;

public class EmailConfirmation
{
    public string Email { get; set; }
    public string Token { get; set; }
}
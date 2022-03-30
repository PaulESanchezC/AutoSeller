using System.ComponentModel.DataAnnotations;

namespace Models.AuthenticationModels;

public class EmailConfirmationDto
{
    [EmailAddress]
    public string Email { get; set; }
    
    public string Token { get; set; }
}
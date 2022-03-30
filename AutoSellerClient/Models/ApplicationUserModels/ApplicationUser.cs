namespace Models.ApplicationUserModels;

public class ApplicationUser
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string StateOrProvince { get; set; }
    public bool HasPassword { get; set; }
    public string Token { get; set; }
    public List<String> Roles { get; set; }
}
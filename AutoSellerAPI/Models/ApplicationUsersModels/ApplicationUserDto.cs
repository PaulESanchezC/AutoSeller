namespace Models.ApplicationUsersModels;

public class ApplicationUserDto
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string StateOrProvince { get; set; }

    public string Email { get; set; }

    public bool HasPassword { get; set; }

    public List<string> Roles { get; set; } = new List<string>();
}
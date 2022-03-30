using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.ApplicationUsersModels;
using Models.JwtServiceConfigurationModel;
using Models.PolicyAuthorizationConfigurationsModels;

namespace Services.JwtServices;

public class JwtServices : IJwtServices
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtServiceConfiguration _jwtOptions;
    private readonly PolicyServiceOptions _policyOptions;
    public JwtServices(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IOptions<JwtServiceConfiguration> jwtOptions,
        IOptions<PolicyServiceOptions> policyOptions)
    {
        _db = db;
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
        _policyOptions = policyOptions.Value;
    }
    public async Task<string> GenerateTokenAsync(ApplicationUser appUser, bool hasPassword)
    {
        var claims = await GenerateClaims(appUser, hasPassword);
        
        var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);
        var symmetricSecurityKey = new SymmetricSecurityKey(key);
        var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.ValidIssuer,
            Audience = _jwtOptions.ValidAudience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = signInCredentials,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.UtcNow,
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private async  Task<IList<Claim>> GenerateClaims(ApplicationUser user, bool hasPassword)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.GivenName, user.FirstName),
            new (ClaimTypes.Surname, user.LastName),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.MobilePhone, user.PhoneNumber),
            new (ClaimTypes.StreetAddress, user.Address),
            new (ClaimTypes.Locality, user.City),
            new (ClaimTypes.StateOrProvince, user.StateOrProvince),
            new (ClaimTypes.Authentication, hasPassword.ToString()),
        };

        var roles = await _userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        if (user.FirstName == _policyOptions.Name)
            claims.Add(new (ClaimTypes.Hash,_policyOptions.Code));

        return claims;
    }
}
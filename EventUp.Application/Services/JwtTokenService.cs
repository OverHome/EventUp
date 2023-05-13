using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EventUp.Application.Services;

public class JwtTokenService : IJwtTokenService
{
    private string _issuer;
    private string _audience;
    private  string _key;
    private int _validTimeInMin;
    
    public JwtTokenService(IConfiguration configuration)
    {
        _issuer = configuration["JwtAuthentication:Issuer"] ?? throw new InvalidOperationException();
        _audience = configuration["JwtAuthentication:Audience"] ?? throw new InvalidOperationException();
        _key = configuration["JwtAuthentication:Key"] ?? throw new InvalidOperationException();
        _validTimeInMin = Convert.ToInt32(configuration["JwtAuthentication:ValidTimeInMin"]);
    }

    private SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(_key));
    
    public async Task<JwtToken> CreateJwtToken(User user)
    {
        var claims = new List<Claim> {new(ClaimTypes.NameIdentifier, user.Id.ToString()), new(ClaimTypes.Name, user.Name), new(ClaimTypes.Role, user.UserRoles.ToString()) };
        var jwt = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_validTimeInMin)),
            signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
            
        return  new JwtToken{UserName=user.Name, Token=new JwtSecurityTokenHandler().WriteToken(jwt)};
    }
}
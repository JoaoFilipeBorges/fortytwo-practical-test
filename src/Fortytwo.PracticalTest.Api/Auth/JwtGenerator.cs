using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fortytwo.PracticalTest.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Fortytwo.PracticalTest.Api.Auth;

public class JwtGenerator(IConfiguration configuration)
{
    public string GenerateJwtToken(User user)
    {
        var jwtKey = configuration["Jwt:Key"];
        var jwtIssuer = configuration["Jwt:Issuer"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtIssuer,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
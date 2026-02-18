using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using server.Models.DTOs;

namespace server.Services;

internal class JwtService(IConfiguration config)
{
    
    private readonly IConfiguration _configuration = config;
    public string GenerateAccessToken(UserValidRequest user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:secrets"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
           Subject = new ClaimsIdentity([
            
               new Claim(ClaimTypes.NameIdentifier, user.Username),
               new (ClaimTypes.Email, user.Email)
            ]),
           Issuer = _configuration["Jwt:iss"],
           Audience = _configuration["Jwt:aud"],
           Expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(_configuration["Jwt:expInSec"])),
           SigningCredentials = new(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature
           )
        };
        var securityHandler = new JwtSecurityTokenHandler();
        var token = securityHandler.CreateToken(tokenDescriptor);
        var accessToken = securityHandler.WriteToken(token);
        return accessToken;
    }
}
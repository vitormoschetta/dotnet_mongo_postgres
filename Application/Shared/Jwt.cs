using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_mongodb.Application.Shared;

public class Jwt
{
    private readonly IOptions<AppSettings> _appSettings;

    public Jwt(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings;

        if (_appSettings.Value.JwtConfig is null)
            throw new Exception("JWT configuration is missing.");
    }

    public Task<string> GenerateToken(string username, string email, string[] roles)
    {
        var key = Encoding.ASCII.GetBytes(_appSettings.Value.JwtConfig.Secret);
        var securityKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, string.Join(",", roles))
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_appSettings.Value.JwtConfig.Expiration),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Task.FromResult(tokenHandler.WriteToken(token));
    }
}

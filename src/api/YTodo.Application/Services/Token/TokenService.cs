using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using YTodo.Application.Options;

namespace YTodo.Application.Services.Token;

public class TokenService(IOptionsMonitor<TokenOptions> options) : ITokenService
{
    public string GenerateAccessToken(int userId)
    {
        var optionsValue = options.CurrentValue;
        
        var claims = new List<Claim> { new("sub", userId.ToString()) };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(optionsValue.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), 
            Expires = DateTime.UtcNow.Add(optionsValue.ExpirationTimeSpan),
            SigningCredentials = credentials,
            Issuer = optionsValue.Issuer,
            Audience = optionsValue.Audience
        };

        return new JsonWebTokenHandler().CreateToken(securityTokenDescriptor);
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
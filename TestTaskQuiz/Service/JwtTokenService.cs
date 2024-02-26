using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TestTaskQuiz.Core.Services;
using TestTaskQuiz.Models.AuthModels;

namespace TestTaskQuiz.Service;

public class JwtTokenService : IJwtTokenGenerator
{
    private readonly JwtConfiguration _configuration;

    public JwtTokenService(JwtConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(IEnumerable<Claim> claims, int tokenDuration)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding
                .UTF8
                .GetBytes(_configuration.SecretKey));
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            notBefore: DateTime.Now,
            claims: claims,
            expires: DateTime.Now.AddSeconds(tokenDuration),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtSecretKey = Encoding.UTF8.GetBytes(_configuration.SecretKey);
            var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(jwtSecretKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidAudience = _configuration.Audience,
                ValidIssuer = _configuration.Issuer,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            }, out var _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
using System.Security.Claims;

namespace TestTaskQuiz.Core.Services;

public interface IJwtTokenGenerator
{
    public string GenerateJwtToken(IEnumerable<Claim> claims, int tokenDuration);
    public bool ValidateToken(string token);
}
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskQuiz.Core.Data;
using TestTaskQuiz.Core.Services;
using TestTaskQuiz.Models;
using TestTaskQuiz.Models.AuthModels;
using Exception = System.Exception;

namespace TestTaskQuiz.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly IUnitOfWorkRepository _uowRepository;
    private readonly IPasswordHash _passwordHash;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUnitOfWorkRepository uowRepository, IPasswordHash passwordHash,
        IJwtTokenGenerator jwtTokenGenerator, ILogger<AuthController> logger)
    {
        _uowRepository = uowRepository;
        _passwordHash = passwordHash;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUser loginUser)
    {
        try
        {
            using var uowRepository = _uowRepository;
            var userRepository = uowRepository.GenericRepository<User>();
            var user = await userRepository.FindAsync(user =>
                user.PasswordHash.Equals(_passwordHash.HashPassword(loginUser.Password)) &&
                user.Email.Equals(loginUser.Email));
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var tokenRepository = uowRepository.GenericRepository<Token>();
            var authClaims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email)
            };
            string refreshToken = _jwtTokenGenerator.GenerateJwtToken(authClaims, 10 * 6000);
            await tokenRepository.CreateAsync(new Token() { UserId = user.Id, RefreshToken = refreshToken });
            await uowRepository.SaveAsync();
            var tokens = new JwtResponse()
            {
                AccessToken = _jwtTokenGenerator.GenerateJwtToken(authClaims, 10 * 60),
                RefreshToken = refreshToken,
                User = new UserDto(user)
            };

            return Ok(tokens);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("Something bad :/");
        }
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(LoginUser loginUser)
    {
        try
        {
            using var uowRepository = _uowRepository;
            var userRepository = uowRepository.GenericRepository<User>();
            await userRepository.CreateAsync(new User()
            {
                Email = loginUser.Email,
                PasswordHash = _passwordHash.HashPassword(loginUser.Password)
            });
            await uowRepository.SaveAsync();
            return Ok(loginUser);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("Something bad :/");
        }
    }

    [HttpPost]
    [Authorize]
    [Route("check")]
    public async Task<IActionResult> CheckAuth()
    {
        return Ok();
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> RefreshTokens(RefreshTokenDto token)
    {
        try
        {
            using var uowRepository = _uowRepository;
            var tokenRepository = uowRepository.GenericRepository<Token>();
            var oldToken =
                await tokenRepository.FindAsync(obj => obj.RefreshToken.Equals(token.Token),
                    new[] { $"{nameof(Token.User)}" });
            if (oldToken?.User == null)
            {
                return BadRequest("Token is not valid");
            }

            await tokenRepository.DeleteAsync(oldToken);
            var authClaims = new[]
            {
                new Claim("UserId", oldToken.UserId.ToString()),
                new Claim("Email", oldToken.User.Email)
            };
            string refreshToken = _jwtTokenGenerator.GenerateJwtToken(authClaims, 10 * 6000);
            await tokenRepository.CreateAsync(new Token() { UserId = oldToken.UserId, RefreshToken = refreshToken });
            await _uowRepository.SaveAsync();
            return Ok(new JwtResponse()
            {
                AccessToken = _jwtTokenGenerator.GenerateJwtToken(authClaims, 10 * 60),
                RefreshToken = refreshToken,
                User = new UserDto(oldToken.User)
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("Something bad :/");
        }
    }
}
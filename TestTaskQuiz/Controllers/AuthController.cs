using Microsoft.AspNetCore.Mvc;
using TestTaskQuiz.Models.AuthModels;

namespace TestTaskQuiz.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    public AuthController()
    {
    }

    public IActionResult Login(LoginUser loginUser)
    {
        return Ok();
    }
}
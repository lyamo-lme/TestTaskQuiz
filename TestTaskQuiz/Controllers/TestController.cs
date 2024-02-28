using Microsoft.AspNetCore.Mvc;
using TestTaskQuiz.Core.Data;

namespace TestTaskQuiz.Controllers;

public class TestController : Controller
{
    private readonly IUnitOfWorkRepository _uowRepository;
    public TestController(IUnitOfWorkRepository uowRepository)
    {
        _uowRepository = uowRepository;
    }
}
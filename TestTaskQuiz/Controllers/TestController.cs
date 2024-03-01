using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTaskQuiz.Core.Data;
using TestTaskQuiz.Models;
using TestTaskQuiz.Models.ApiDto.Test;
using TestTaskQuiz.Models.ApiDto.Test.ForUser;
using TestTaskQuiz.Service;

namespace TestTaskQuiz.Controllers;

[ApiController]
[Route("test")]
public class TestController : Controller
{
    private readonly IUnitOfWorkRepository _uowRepository;
    private readonly IMapper _mapper;

    public TestController(IUnitOfWorkRepository uowRepository)
    {
        _mapper = MapperFactory.CreateMapper<AppAutoMapper>();
        _uowRepository = uowRepository;
    }


    [HttpPost]
    public async Task<IActionResult> CreateTest(TestDto<QuestionDto> testDto)
    {
        try
        {
            var newTest = _mapper.Map<Test>(testDto);
            await _uowRepository.GenericRepository<Test>().CreateAsync(newTest);
            await _uowRepository.SaveAsync();
            return Ok(newTest);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }


    [HttpGet]
    [Route("user/{userId}")]
    public async Task<IActionResult> GetUsersTest(int userId)
    {
        try
        {
            var usersTests = await _uowRepository
                .GenericRepository<UsersTest>()
                .GetAsync(x => x.UserId == userId, includeProperties: $"{nameof(UsersTest.Test)}");
            var query = usersTests.ToList();
            var tests = _mapper.Map<List<UserTestDto>>(query.Select(x => x.Test).ToList());
            try
            {
                foreach (var test in tests)
                {
                    var testAnswers = await _uowRepository.GenericRepository<UsersAnswers>()
                        .GetAsync(x => x.QuestionAnswer.TestQuestion.TestId == test.Id);
                    int correctUserAnswers = 0;
                    var answerIds = testAnswers.Select(x => x.AnswerId).ToList();
                    var correctAnswers = await _uowRepository.GenericRepository<QuestionAnswer>()
                        .GetAsync(x => x.TestQuestion.TestId == test.Id && x.IsCorrect == true);
                    foreach (var answerId in answerIds)
                        if (correctAnswers.Any(x => x.Id == answerId))
                            correctUserAnswers++;
                    var testAnswersCount = (await _uowRepository.GenericRepository<TestQuestion>()
                        .GetAsync(x => x.TestId == test.Id)).Count();
                    test.CountQuestions = testAnswersCount;
                    test.CorrectAnswers = answerIds.Any() ? correctUserAnswers : null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return Ok(tests);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("answer")]
    public async Task<IActionResult> AddAnswer(UsersAnswers usersAnswers)
    {
        try
        {
            if (await _uowRepository
                    .GenericRepository<UsersAnswers>()
                    .FindAsync(x => x.AnswerId == usersAnswers.AnswerId && x.UserId == usersAnswers.UserId) != null)
            {
                return BadRequest("have answer");
            }

            await _uowRepository.GenericRepository<UsersAnswers>().CreateAsync(new UsersAnswers()
            {
                UserId = usersAnswers.UserId,
                AnswerId = usersAnswers.AnswerId
            });
            await _uowRepository.SaveAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("user")]
    public async Task<IActionResult> AddUserToTest(UsersTest usersTest)
    { 
        try
        {  
            await _uowRepository
                .GenericRepository<UsersTest>()
                .CreateAsync(new UsersTest
                {
                    UserId = usersTest.UserId,
                    TestId = usersTest.TestId,
                });
            await _uowRepository.SaveAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("attempt")]
    public async Task<IActionResult> BeginAttempt(UserTestAttempt usersTest)
    {
        try
        {
            var attemp = await _uowRepository.GenericRepository<UserTestAttempt>().FindAsync(x =>
                x.UserId == usersTest.UserId
                && x.TestId == usersTest.TestId);
            if (attemp == null)
            {
                await _uowRepository.GenericRepository<UserTestAttempt>().CreateAsync(
                    new UserTestAttempt()
                    {
                        UserId = usersTest.UserId,
                        TestId = usersTest.TestId,
                        StartAttemptTime = DateTime.Now
                    });
                await _uowRepository.SaveAsync();
            }

            var attempt = await _uowRepository
                .GenericRepository<UserTestAttempt>()
                .FindAsync(test => test.TestId == usersTest.TestId, new[]
                {
                    $"{nameof(UserTestAttempt.Test)}" +
                    $".{nameof(Test.TestQuestions)}" +
                    $".{nameof(TestQuestion.QuestionAnswers)}"
                });

            return Ok(new
            {
                attempt.Id,
                attempt.StartAttemptTime,
                test = new
                {
                    attempt.Test.Id,
                    attempt.Test.DurationInMinutes,
                    attempt.Test.TestName,
                    testQuestions = attempt.Test.TestQuestions.Select(obj => new
                    {
                        obj.Id,
                        obj.QuestionText,
                        questionAnswer = obj.QuestionAnswers.Select(answer => new
                        {
                            answer.Id,
                            answer.AnswerText
                        })
                    })
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
}
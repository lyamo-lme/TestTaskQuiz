using TestTaskQuiz.Models.AuthModels;

namespace TestTaskQuiz.Models.ApiDto.Test.ForUser;

public class UserTestAttemptDto
{
    public int Id { get; set; }
    public int TestId { get; set; }
    public int ResultInPercent { get; set; }
    public DateTime StartAttemptTime { get; set; }
    public TestDto<UsersQuestionDto>? Test { get; set; }
}
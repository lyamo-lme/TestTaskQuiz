namespace TestTaskQuiz.Models.ApiDto.Test.ForUser;

public class UsersQuestionDto
{
    public string QuestionText { get; set; }
    public IEnumerable<UsersAnswersDto>? QuestionAnswers { get; set; }
}
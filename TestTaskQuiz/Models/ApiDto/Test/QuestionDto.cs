namespace TestTaskQuiz.Models.ApiDto.Test;

public class QuestionDto
{
    public string QuestionText { get; set; }
    public IEnumerable<AnswersDto>? QuestionAnswers { get; set; }
}
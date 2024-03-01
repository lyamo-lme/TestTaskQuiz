namespace TestTaskQuiz.Models.ApiDto.Test.Attemp;

public class TestQuestionInfo
{
    
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public IEnumerable<QuestionAnswerInfo> QuestionAnswers { get; set; }

}
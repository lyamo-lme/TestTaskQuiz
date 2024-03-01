namespace TestTaskQuiz.Models;

public class TestQuestion
{
    public int Id { get; set; }
    public int TestId { get; set; }
    public string QuestionText { get; set; }
    public IEnumerable<QuestionAnswer>? QuestionAnswers { get; set; }
    public Test? Test { get; set; }
}
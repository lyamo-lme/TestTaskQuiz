namespace TestTaskQuiz.Models;

public class QuestionAnswer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string AnswerText { get; set; }
    public IEnumerable<UsersAnswers> UsersAnswers { get; set; }
    public TestQuestion? TestQuestion { get; set; }
}
namespace TestTaskQuiz.Models;

public class UsersAnswers
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AnswerId { get; set; }
    public User? User { get; set; }
    public QuestionAnswer? QuestionAnswer { get; set; }
}
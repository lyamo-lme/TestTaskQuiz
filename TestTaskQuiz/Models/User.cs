namespace TestTaskQuiz.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public IEnumerable<UsersTest> UsersTests { get; set; }
    public IEnumerable<UsersAnswers> UsersAnswers { get; set; }
    public IEnumerable<Token> Tokens { get; set; }
}
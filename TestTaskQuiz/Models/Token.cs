namespace TestTaskQuiz.Models;

public class Token
{
    public string RefreshToken { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
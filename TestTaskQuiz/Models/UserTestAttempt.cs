namespace TestTaskQuiz.Models;

public class UserTestAttempt
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TestId { get; set; }
    public int ResultInPercent { get; set; }
    public DateTime StartAttemptTime { get; set; }
    public User? User { get; set; }
    public Test? Test { get; set; }
}
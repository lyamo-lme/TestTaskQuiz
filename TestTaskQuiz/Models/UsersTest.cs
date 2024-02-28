namespace TestTaskQuiz.Models;

public class UsersTest
{
    public int Id { get; set; }
    public int TestId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public Test? Test { get; set; }
}
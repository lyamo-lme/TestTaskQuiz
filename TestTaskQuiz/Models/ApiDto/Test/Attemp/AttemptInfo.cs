namespace TestTaskQuiz.Models.ApiDto.Test.Attemp;

public class AttemptInfo
{
    public int Id { get; set; }
    public DateTime StartAttemptTime { get; set; }
    public TestInfo Test { get; set; }
}
namespace TestTaskQuiz.Models.ApiDto.Test;

public class TestDto<T>
{
    public string TestName { get; set; }
    public int DurationInMinutes { get; set; }
    public IEnumerable<T>? TestQuestions { get; set; }
}
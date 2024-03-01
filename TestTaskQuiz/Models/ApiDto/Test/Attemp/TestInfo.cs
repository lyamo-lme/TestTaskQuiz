namespace TestTaskQuiz.Models.ApiDto.Test.Attemp;

public class TestInfo
{
    public int Id { get; set; }
    public int DurationInMinutes { get; set; }
    public string TestName { get; set; }
    public IEnumerable<TestQuestionInfo> TestQuestions { get; set; }

}
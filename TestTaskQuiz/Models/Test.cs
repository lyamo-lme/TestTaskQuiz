namespace TestTaskQuiz.Models;

public class Test
{
    public int Id { get; set; }
    public string TestName { get; set; }
    public IEnumerable<UsersTest> UsersTests { get; set; }
    public IEnumerable<TestQuestion> TestQuestions { get; set; }

}
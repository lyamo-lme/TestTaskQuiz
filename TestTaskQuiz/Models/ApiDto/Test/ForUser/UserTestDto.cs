namespace TestTaskQuiz.Models.ApiDto.Test.ForUser;

public class UserTestDto
{
    public int Id { get; set; }
    public int DurationInMinutes { get; set; }
    public int? CountQuestions { get; set; }
    public int? CorrectAnswers  { get; set; }
    public string TestName { get; set; }
}
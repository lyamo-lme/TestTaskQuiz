namespace TestTaskQuiz.Models.AuthModels;

public class JwtResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public UserDto User;
}
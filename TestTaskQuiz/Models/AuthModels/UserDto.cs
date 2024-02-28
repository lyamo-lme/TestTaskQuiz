namespace TestTaskQuiz.Models.AuthModels;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }

    public UserDto(User user)
    {
        Id = user.Id;
        Email = user.Email;
    }
}
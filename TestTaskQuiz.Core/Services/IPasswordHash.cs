namespace TestTaskQuiz.Core.Services;

public interface IPasswordHash
{
    public bool ComparePassword(string hashPassword, string password);
    public string HashPassword(string password);
}
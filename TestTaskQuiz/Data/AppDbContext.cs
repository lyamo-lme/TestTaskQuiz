using Microsoft.EntityFrameworkCore;
using TestTaskQuiz.Data.Config;
using TestTaskQuiz.Models;

namespace TestTaskQuiz.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionAnswersConfiguration());
        modelBuilder.ApplyConfiguration(new TestConfiguration());
        modelBuilder.ApplyConfiguration(new TestQuestionsConfiguration());
        modelBuilder.ApplyConfiguration(new UserTestAttemptConfiguration());
        modelBuilder.ApplyConfiguration(new UsersTestConfiguration());
        modelBuilder.ApplyConfiguration(new UsersAnswersConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<UsersTest> UsersTests { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<UserTestAttempt> UserTestAttempts { get; set; }
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public DbSet<TestQuestion> TestQuestions { get; set; }
    public DbSet<UsersAnswers> UsersAnswers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
}
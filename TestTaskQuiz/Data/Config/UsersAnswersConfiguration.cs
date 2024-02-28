using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskQuiz.Models;

namespace TestTaskQuiz.Data.Config;

public class UsersAnswersConfiguration : IEntityTypeConfiguration<UsersAnswers>
{
    public void Configure(EntityTypeBuilder<UsersAnswers> builder)
    {
        builder.HasKey(key => key.Id);
        builder.HasOne<User>(prop => prop.User)
            .WithMany(prop => prop.UsersAnswers)
            .HasForeignKey(fKey => fKey.UserId);

        builder.HasOne<QuestionAnswer>(prop => prop.QuestionAnswer)
            .WithMany(prop => prop.UsersAnswers)
            .HasForeignKey(fKey => fKey.UserId);
    }
}
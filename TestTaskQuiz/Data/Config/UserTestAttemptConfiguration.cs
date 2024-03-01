using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskQuiz.Models;

namespace TestTaskQuiz.Data.Config;

public class UserTestAttemptConfiguration : IEntityTypeConfiguration<UserTestAttempt>
{
    public void Configure(EntityTypeBuilder<UserTestAttempt> builder)
    {
        builder.HasKey(prop => prop.Id);

        builder.HasOne<Test>(prop => prop.Test)
            .WithMany(prop => prop.UserTestAttempts)
            .HasForeignKey(key => key.TestId);

        builder.HasOne<User>(prop => prop.User)
            .WithMany(prop => prop.UserTestAttempts)
            .HasForeignKey(key => key.UserId);
    }
}
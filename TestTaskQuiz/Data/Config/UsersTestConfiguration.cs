using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskQuiz.Models;

namespace TestTaskQuiz.Data.Config;

public class UsersTestConfiguration
    : IEntityTypeConfiguration<UsersTest>
{
    public void Configure(EntityTypeBuilder<UsersTest> builder)
    {
        builder.HasKey(prop => prop.Id);

        builder.HasOne<Test>(prop => prop.Test)
            .WithMany(prop => prop.UsersTests)
            .HasForeignKey(key => key.TestId);

        builder.HasOne<User>(prop => prop.User)
            .WithMany(prop => prop.UsersTests)
            .HasForeignKey(key => key.UserId);
    }
}
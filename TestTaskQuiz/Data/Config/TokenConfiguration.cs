using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskQuiz.Models;

namespace TestTaskQuiz.Data.Config;

public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.Property(prop => prop.UserId).IsRequired();
        
        builder.HasOne<User>(prop => prop.User)
            .WithMany(prop => prop.Tokens)
            .HasForeignKey(prop => prop.UserId);
    }
}
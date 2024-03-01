using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskQuiz.Models;

namespace TestTaskQuiz.Data.Config;

public class TestQuestionsConfiguration : IEntityTypeConfiguration<TestQuestion>
{
    public void Configure(EntityTypeBuilder<TestQuestion> builder)
    {
        builder.HasKey(key => key.Id);

        builder.HasMany<QuestionAnswer>(prop => prop.QuestionAnswers)
            .WithOne(prop => prop.TestQuestion)
            .HasForeignKey(key => key.QuestionId);
    }
}
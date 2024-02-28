using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskQuiz.Models;

namespace TestTaskQuiz.Data.Config;

public class QuestionAnswersConfiguration :IEntityTypeConfiguration<QuestionAnswer>{
    public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
    {
        builder.HasKey(key => key.Id);
        builder.HasOne<TestQuestion>(prop => prop.TestQuestion)
            .WithMany(prop => prop.QuestionAnswers)
            .HasForeignKey(fKey => fKey.QuestionId);
    }
}
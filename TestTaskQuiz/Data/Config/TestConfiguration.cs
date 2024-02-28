using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskQuiz.Models;

namespace TestTaskQuiz.Data.Config;

public class TestConfiguration: IEntityTypeConfiguration<Test>

{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasKey(prop => prop.Id);
        
    }
}
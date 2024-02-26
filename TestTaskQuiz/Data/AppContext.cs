using Microsoft.EntityFrameworkCore;
using TestTaskQuiz.Data.Config;
using TestTaskQuiz.Models;

namespace TestTaskQuiz.Data;

public class AppContext:DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        base.OnModelCreating(modelBuilder);
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
}
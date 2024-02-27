using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using TestTaskQuiz.Core.Data;
using TestTaskQuiz.Core.Services;
using TestTaskQuiz.Data;
using TestTaskQuiz.Data.Repository;
using TestTaskQuiz.Extensions;
using TestTaskQuiz.Models.AuthModels;
using TestTaskQuiz.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtConfiguration = builder.Configuration.GetSection("Jwt").Get<JwtConfiguration>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>((options) =>
{
    options.UseInMemoryDatabase("Quiz");
    // options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenDocs();

builder.Services.AddSingleton<IJwtTokenGenerator>(new JwtTokenService(jwtConfiguration));
builder.Services.AddSingleton<IPasswordHash, PasswordHash>();
builder.Services.AddTransient<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfiguration.Issuer,
        ValidAudience = jwtConfiguration.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey)),
        ClockSkew = TimeSpan.Zero
    };
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
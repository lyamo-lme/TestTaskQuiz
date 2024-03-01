using AutoMapper;
using TestTaskQuiz.Models;
using TestTaskQuiz.Models.ApiDto.Test;
using TestTaskQuiz.Models.ApiDto.Test.ForUser;

namespace TestTaskQuiz.Service;

public class AppAutoMapper : Profile
{
    public AppAutoMapper()
    {
        CreateMap<AnswersDto, QuestionAnswer>().ReverseMap();
        CreateMap<UsersAnswersDto, QuestionAnswer>().ReverseMap();

        CreateMap<TestDto<QuestionDto>, Test>().ReverseMap();
        CreateMap<TestDto<UsersQuestionDto>, Test>().ReverseMap();
        CreateMap<Test, UserTestDto>().ReverseMap();

        CreateMap<UserTestAttempt, UserTestAttemptDto>().ReverseMap();

        CreateMap<QuestionDto, TestQuestion>().ReverseMap();
        CreateMap<UsersQuestionDto, TestQuestion>().ReverseMap();
    }
}
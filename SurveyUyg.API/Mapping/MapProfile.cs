using AutoMapper;
using SurveyUyg.API.DTOs;
using SurveyUyg.API.Models;

namespace SurveyUyg.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<OptionDto, QuestionOption>()
                .ForMember(dest => dest.QuestionOptionId, opt => opt.MapFrom(src => src.OptionId))
                .ForMember(dest => dest.OptionText, opt => opt.MapFrom(src => src.OptionText))
                .ReverseMap();

            CreateMap<QuestionCreateDto, Question>()
                .ForMember(dest => dest.QuestionOptions, opt => opt.MapFrom(src => src.QuestionOptions))
                .ReverseMap();

            CreateMap<SurveyCreateDto, Survey>()
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId));

            CreateMap<Survey, SurveyListDto>().ReverseMap();
            CreateMap<Survey, SurveyDetailDto>().ReverseMap();
            CreateMap<Question, QuestionListDto>().ReverseMap();
            CreateMap<Answer, AnswerCreateDto>().ReverseMap();

            CreateMap<Question, QuestionGetDto>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.QuestionType))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.QuestionOptions));
        }
    }
}
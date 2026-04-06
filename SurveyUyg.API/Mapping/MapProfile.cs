using AutoMapper;
using SurveyUyg.API.DTOs;
using SurveyUyg.API.Models;

namespace SurveyUyg.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<SurveyCreateDto, Survey>()
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Survey, SurveyListDto>().ReverseMap();
            CreateMap<Survey, SurveyDetailDto>().ReverseMap();

            CreateMap<Question, QuestionListDto>().ReverseMap();
            CreateMap<Question, QuestionCreateDto>().ReverseMap();

            CreateMap<Answer, AnswerCreateDto>().ReverseMap();

            CreateMap<Question, QuestionGetDto>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.QuestionType))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.QuestionOptions)); 

            CreateMap<QuestionOption, OptionDto>()
                .ForMember(dest => dest.OptionId, opt => opt.MapFrom(src => src.QuestionOptionId))
                .ForMember(dest => dest.OptionText, opt => opt.MapFrom(src => src.OptionText));
        }
    }
}
using SurveyUyg.API.DTOs;

namespace SurveyUyg.API.Repositories
{
    public interface IAnswerRepository
    {
        bool SaveAnswers(SurveyResponseDto responseDto);
    }
}
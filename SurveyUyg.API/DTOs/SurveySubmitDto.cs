namespace SurveyUyg.API.DTOs
{
    public class SurveySubmitDto
    {
        public int SurveyId { get; set; }
        public List<AnswerCreateDto> Answers { get; set; }
    }
}

namespace SurveyUyg.API.DTOs
{
    public class SurveyDetailDto
    {
        public int SurveyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<QuestionGetDto> Questions { get; set; } = new List<QuestionGetDto>();
    }
}
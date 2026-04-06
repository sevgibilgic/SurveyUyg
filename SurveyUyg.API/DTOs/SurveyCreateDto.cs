namespace SurveyUyg.API.DTOs
{
    public class SurveyCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? AppUserId { get; set; }
        public List<QuestionCreateDto> Questions { get; set; }

    }
}
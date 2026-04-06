namespace SurveyUyg.API.DTOs
{
    public class SurveyListDto
    {
        public int SurveyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int QuestionCount { get; set; }
    }
}
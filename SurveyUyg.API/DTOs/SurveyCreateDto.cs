namespace SurveyUyg.API.DTOs
{
    public class SurveyCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
    }
}
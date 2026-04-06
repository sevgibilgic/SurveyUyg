namespace SurveyUyg.API.DTOs
{
    public class QuestionListDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int SurveyId { get; set; }
    }
}
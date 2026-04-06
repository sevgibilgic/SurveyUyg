namespace SurveyUyg.API.DTOs
{
    public class QuestionCreateDto
    {
        public string QuestionText { get; set; }
        public List<OptionDto> QuestionOptions { get; set; }
    }
}
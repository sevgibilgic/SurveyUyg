namespace SurveyUyg.API.DTOs
{
    public class QuestionGetDto
    {
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }
        public List<OptionDto> Options { get; set; } = new List<OptionDto>();
    }
}
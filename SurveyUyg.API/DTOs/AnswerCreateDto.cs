namespace SurveyUyg.API.DTOs
{
    public class AnswerCreateDto
    {
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }
        public string? AnswerText { get; set; }
    }

    public class SurveyResponseDto
    {
        public int SurveyId { get; set; }
        public string UserId { get; set; }
        public List<AnswerCreateDto> Answers { get; set; } = new List<AnswerCreateDto>();
    }
}
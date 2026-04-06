namespace SurveyUyg.API.DTOs
{
    public class ResultDto
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public string? ErrorDetail { get; set; }
    }
}
namespace SurveyUyg.API.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public string QuestionText { get; set; }
        public string? QuestionType { get; set; }
        public int OrderIndex { get; set; }
        public bool IsRequired { get; set; }

        public virtual Survey Survey { get; set; }

        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}

namespace SurveyUyg.API.Models
{
    public class QuestionOption
    {
        public int QuestionOptionId { get; set; }
        public int QuestionId { get; set; }
        public string OptionText { get; set; }
        public int OrderIndex { get; set; }
        public virtual Question Question { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}

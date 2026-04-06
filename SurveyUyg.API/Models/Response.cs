namespace SurveyUyg.API.Models
{
    public class Response
    {
        public int ResponseId { get; set; }
        public int SurveyId { get; set; }
        public string AppUserId { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        public virtual Survey Survey { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }
}

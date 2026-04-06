using SurveyUyg.API.Models;

public class Answer
{
    public int AnswerId { get; set; }
    public int ResponseId { get; set; }
    public int QuestionId { get; set; }
    public string? AnswerText { get; set; }
    public int? SelectedOptionId { get; set; }
    public string? AppUserId { get; set; }

    public virtual Response Response { get; set; }
    public virtual Question Question { get; set; }
    public virtual QuestionOption? SelectedOption { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
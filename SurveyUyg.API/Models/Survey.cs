using System.ComponentModel.DataAnnotations.Schema;
using Azure;

namespace SurveyUyg.API.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace SurveyUyg.API.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UserRole { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();

        public string FullName { get; set; }
    }
}
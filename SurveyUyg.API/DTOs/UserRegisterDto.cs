namespace SurveyUyg.API.Models // Namespace adınızın doğruluğundan emin olun
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
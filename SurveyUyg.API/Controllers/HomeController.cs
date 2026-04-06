using Microsoft.AspNetCore.Mvc;

namespace SurveyUyg.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

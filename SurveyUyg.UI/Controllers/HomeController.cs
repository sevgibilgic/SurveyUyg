using Microsoft.AspNetCore.Mvc;

namespace SurveyUyg.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _apiBaseUrl = "https://localhost:7184/api";
        private dynamic id;

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddSurvey()
        {
            return View();
        }

        public IActionResult SurveysControl()
        {
            return View();
        }
        public IActionResult SurveyDetail()
        {
            ViewBag.SurveyId = id;
            return View();
        }

        public IActionResult Surveys()
        {
            return View();
        }
        public IActionResult MySurveys()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Login()
        {
            ViewBag.ApiBaseURL = _apiBaseUrl;
            return View();
        }

        public IActionResult SignIn()
        {
            ViewBag.ApiBaseURL = _apiBaseUrl;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
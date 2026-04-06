using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SurveyUyg.UI.Models;

namespace SurveyUyg.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();
            // Buradaki URL, senin API projenin çalıştığı URL olmalı (Swagger'dan bakabilirsin)
            var response = await client.GetAsync("https://localhost:7184/api/Surveys");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ViewBag.Data = content; // Ham JSON verisini sayfaya gönderip test edelim
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

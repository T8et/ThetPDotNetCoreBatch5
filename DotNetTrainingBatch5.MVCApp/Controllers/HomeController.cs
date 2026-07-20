using DotNetTrainingBatch5.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotNetTrainingBatch5.MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Name = "ThetPan";

            HomeResponseModel model = new HomeResponseModel();
            model.txtMessage = "Welcome to ASP.NET Core MVC Application";
            return View(model);
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

        public IActionResult Index2()
        {
            return View();
        }
    }
}

using DotNetTrainingBatch5.Database.Models;
using DotNetTrainingBatch5.Shared;
using DotNetTrainingBatch5.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DotNetTrainingBatch5.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestClientServices _resapi;

        public HomeController(ILogger<HomeController> logger, IRestClientServices resapi)
        {
            _logger = logger;
            _resapi = resapi;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _resapi.SendAsync<TblBlog[]>("api/Blog", ReqType.GET);
            _logger.LogInformation(JsonConvert.SerializeObject(blogs));
            return View(blogs);
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

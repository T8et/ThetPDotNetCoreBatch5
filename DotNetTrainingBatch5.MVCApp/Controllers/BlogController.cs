using DotNetTrainingBatch5.Common.Features.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTrainingBatch5.MVCApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogServices _blogServices;

        public BlogController(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        public IActionResult Index()
        {
            var lst = _blogServices.getBlogs();
            return View(lst);
        }
    }
}

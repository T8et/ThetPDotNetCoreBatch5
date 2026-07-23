using DotNetTrainingBatch5.Common.Features.Blogs;
using DotNetTrainingBatch5.Database.Models;
using DotNetTrainingBatch5.MVCApp.Models;
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

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogRequestModel blog)
        {
            try
            {
                _blogServices.createBlog(new TblBlog
                {
                    BlogAuthor = blog.Author!,
                    BlogContent = blog.Description,
                    BlogTitle = blog.Title!,
                    DeleteFlag = false
                });

                //ViewBag.isSuccess = true;
                //ViewBag.message = "Blog Created Successfully";

                TempData["isSuccess"] = true;
                TempData["message"] = "Blog Created Successfully";
            }
            catch (Exception ex) 
            {
                TempData["isSuccess"] = false;
                TempData["message"] = ex.Message.ToString();
            }
                      
            return RedirectToAction("Index");
        }


        [ActionName("Delete")]
        public IActionResult Delete(int id) 
        {
            try
            {
                _blogServices.deleteBlog(id);
                TempData["isSuccess"] = true;
                TempData["message"] = "Blog deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["isSuccess"] = false;
                TempData["message"] = ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }
    }
}

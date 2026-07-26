using DotNetTrainingBatch5.Common.Features.Blogs;
using DotNetTrainingBatch5.Database.Models;
using DotNetTrainingBatch5.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTrainingBatch5.MVCApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly IBlogServices _blogServices;

        public BlogAjaxController(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        // CRUD
        // Read
        public IActionResult Index()
        {
            var lst = _blogServices.getBlogs();
            return View("BlogList",lst);
        }

        [ActionName("List")]
        public IActionResult BlogListAjax() 
        {
            var lst = _blogServices.getBlogs();
            return Json(lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("AjaxBlogCreate");
        }

        [HttpPost]
        [ActionName("ASave")]
        public IActionResult BlogSave(BlogRequestModel blog)
        {
            MessageModel msgMdl;
            try
            {
                _blogServices.createBlog(new TblBlog
                {
                    BlogAuthor = blog.Author,
                    BlogContent = blog.Description,
                    BlogTitle = blog.Title,
                    DeleteFlag = false
                });

                //ViewBag.isSuccess = true;
                //ViewBag.message = "Blog Created Successfully";

                TempData["isSuccess"] = true;
                TempData["message"] = "Blog Created Successfully";

                msgMdl = new MessageModel(true, "Blog Created Successfully");
            }
            catch (Exception ex) 
            {
                TempData["isSuccess"] = false;
                TempData["message"] = ex.Message.ToString();

                msgMdl = new MessageModel(false, ex.Message.ToString());
            }
                      
            return Json(msgMdl);
        }

        public class MessageModel
        {
            public MessageModel() { }

            public MessageModel(bool isSuccess, string message)
            {
                this.isSuccess = isSuccess;
                this.Message = message;
            }
            public bool isSuccess { get; set; }
            public string Message { get; set; }
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete(BlogRequestModel model) 
        {
            MessageModel msgMdl;
            try
            {
                _blogServices.deleteBlog(model.Id);
                TempData["isSuccess"] = true;
                TempData["message"] = "Blog deleted successfully";
                msgMdl = new MessageModel(true, "Blog deleted successfully");
            }
            catch (Exception ex)
            {
                TempData["isSuccess"] = false;
                TempData["message"] = ex.Message.ToString();
                msgMdl = new MessageModel(false, ex.Message.ToString());
            }
            return Json(msgMdl);
        }

        [ActionName("Edit")]
        public IActionResult Edit(int id)
        {
            var data = _blogServices.getBlog(id).FirstOrDefault();
            if (data is null)
            {
                TempData["isSuccess"] = false;
                TempData["message"] = "Blog not found";
                return RedirectToAction("Index");
            }
            var model = new BlogRequestModel
            {
                Id = data.BlogId,
                Author = data.BlogAuthor,
                Description = data.BlogContent,
                Title = data.BlogTitle
            };
            return View("AjaxBlogEdit", model);
        }

        [HttpPost]
        [ActionName("AUpdate")]
        public IActionResult Update(int id, BlogRequestModel blog)
        {
            MessageModel msgMdl;
            try
            {
                _blogServices.patchBlog(id, new TblBlog
                {
                    BlogAuthor = blog.Author!,
                    BlogContent = blog.Description,
                    BlogTitle = blog.Title!
                });
                TempData["isSuccess"] = true;
                TempData["message"] = "Blog updated successfully";

                msgMdl = new MessageModel(true, "Blog updated successfully");
            }
            catch (Exception ex)
            {
                TempData["isSuccess"] = false;
                TempData["message"] = ex.Message.ToString();
                msgMdl = new MessageModel(false, ex.Message.ToString());
            }
            return Json(msgMdl);
        }
    }
}

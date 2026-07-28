using DotNetTrainingBatch5.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTrainingBatch5.APISRVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            return Ok(new TblBlog[]
            {
                new TblBlog { BlogId = 1, BlogTitle = "First Blog", BlogContent = "This is the content of the first blog.", BlogAuthor = "John Doe" },
                new TblBlog { BlogId = 2, BlogTitle = "Second Blog", BlogContent = "This is the content of the second blog.", BlogAuthor = "Jane Smith" },
                new TblBlog { BlogId = 3, BlogTitle = "Third Blog", BlogContent = "This is the content of the third blog.", BlogAuthor = "Bob Johnson" }
            });
        }
    }
}

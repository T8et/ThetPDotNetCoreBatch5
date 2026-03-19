using DotNetTrainingBatch5.Common.Features.Blogs;
using DotNetTrainingBatch5.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.RESTAPI.Controllers
{
    //Presentation layer
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServicesController : ControllerBase
    {
        private readonly BlogServices _service;

        public BlogServicesController()
        {
            _service = new BlogServices();
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var list = _service.getBlogs();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            var list = _service.getBlog(id);
            if (list is null)
            {
                return BadRequest();
            }
            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog tblBlog)
        {
            _service.createBlog(tblBlog);
            return Ok(tblBlog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog tblblog)
        {
            var list = _service.updateBlog(id, tblblog);
            if (list is null)
            {
                return BadRequest();
            }

            return Ok(list);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog tblBlog)
        {
            var list = _service.patchBlog(id, tblBlog);
            if (list is null)
            {
                return BadRequest();
            }

            return Ok(list);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var list = _service.deleteBlog(id);
            if (list is null)
            {
                return BadRequest();
            }

            return Ok(list);
        }
    }
}

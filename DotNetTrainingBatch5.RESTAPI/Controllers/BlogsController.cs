using DotNetTrainingBatch5.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        protected readonly AppDBContext _db = new AppDBContext();
        [HttpGet]
        public IActionResult GetBlogs() 
        {
            var list = _db.TblBlogs.AsNoTracking().Where(x=>x.DeleteFlag == false).ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            var list = _db.TblBlogs.AsNoTracking().Where(x => x.DeleteFlag == false && x.BlogId == id).FirstOrDefault();
            if(list is null)
            {
                return BadRequest();
            }
            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog tblBlog)
        {
            _db.TblBlogs.Add(tblBlog);
            _db.SaveChanges();
            return Ok(tblBlog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,TblBlog tblblog)
        {
            var list = _db.TblBlogs.AsNoTracking().Where(x => x.DeleteFlag == false && x.BlogId == id).FirstOrDefault();
            if (list is null)
            {
                return BadRequest();
            }

            list.BlogTitle = tblblog.BlogTitle;
            list.BlogAuthor = tblblog.BlogAuthor;
            list.BlogContent = tblblog.BlogContent;

            _db.Entry(list).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(list);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog tblBlog)
        {
            var list = _db.TblBlogs.AsNoTracking().Where(x => x.DeleteFlag == false && x.BlogId == id).FirstOrDefault();
            if (list is null)
            {
                return BadRequest();
            }

            if(tblBlog.BlogTitle is not null)
            {
                list.BlogTitle = tblBlog.BlogTitle;
            }
            if (tblBlog.BlogAuthor is not null)
            {
                list.BlogAuthor = tblBlog.BlogAuthor;
            }
            if (tblBlog.BlogContent is not null)
            {
                list.BlogContent = tblBlog.BlogContent;
            }

            _db.Entry(list).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var list = _db.TblBlogs.AsNoTracking().Where(x => x.DeleteFlag == false && x.BlogId == id).FirstOrDefault();
            if (list is null)
            {
                return BadRequest();
            }

            list.DeleteFlag = true;
            _db.Entry(list).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(list);
        }
    }
}

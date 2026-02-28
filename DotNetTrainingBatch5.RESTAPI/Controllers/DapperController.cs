using Dapper;
using DotNetTrainingBatch5.RESTAPI.Data_Model;
using DotNetTrainingBatch5.RESTAPI.View_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace DotNetTrainingBatch5.RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        protected readonly string _connection = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True";

        [HttpGet]
        public ActionResult Get() 
        {
            string query = "Select * from Tbl_Blog where DeleteFlag = 0";
            using(IDbConnection db = new SqlConnection(_connection))
            {
                var list = db.Query(query).ToList();
                return Ok(list);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetBlog(int id) 
        {
            string query = "Select * from Tbl_Blog where DeleteFlag = 0 and BlogId=@id";
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var list = db.Query(query, new { id }).ToList();
                return Ok(list);
            }
        }

        [HttpPost]
        public IActionResult Create(string title, string author, string content)
        {
            string insquery = $@"insert into [dbo].[tbl_blog]
                                   ([blogtitle]
                                   ,[blogauthor]
                                   ,[blogcontent]
                                   ,[deleteflag])
                                values
                                   (@blogtitle 
                                   ,@blogauthor
                                   ,@blogcontent
                                   ,0)";

            using(IDbConnection db = new SqlConnection(_connection))
            {
                var result = db.Execute(insquery, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

                if (result > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }   
            }
        }

        [HttpPut]
        public IActionResult Update(int id, string title, string author, string content)
        {
            string upd = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @blogTitle
                              ,[BlogAuthor] = @blogAuthor
                              ,[BlogContent] = @blogContent
                              ,[DeleteFlag] = 0
                           WHERE [BlogId] = @blogId";
            using (IDbConnection db = new SqlConnection(_connection))
            {
                var result = db.Execute(upd, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                    BlogId = id
                });

                if (result > 0)
                {
                    return Ok();
                }
                else return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            string delquery = @"Delete from TBL_BLOG where BlogId=@BlogId";
            using(IDbConnection db = new SqlConnection(_connection))
            {
                int result = db.Execute(delquery, new
                {
                    BlogId = id
                });
                if(result > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}

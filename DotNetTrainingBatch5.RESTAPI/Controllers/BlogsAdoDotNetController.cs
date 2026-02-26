using DotNetTrainingBatch5.RESTAPI.Data_Model;
using DotNetTrainingBatch5.RESTAPI.View_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DotNetTrainingBatch5.RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        protected readonly string _connection = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True";

        [HttpGet]
        public IActionResult Get()
        {
            List<BlogViewModel> blogdata = new List<BlogViewModel>();

            string query = "Select * from Tbl_Blog where DeleteFlag=0";

            SqlConnection con = new SqlConnection(_connection);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                blogdata.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(rdr["BlogId"]),
                    Title = Convert.ToString(rdr["BlogTitle"]),
                    Author = Convert.ToString(rdr["BlogAuthor"]),
                    Content = Convert.ToString(rdr["BlogContent"]),
                    Flag = Convert.ToInt32(rdr["DeleteFlag"])
                });
            }

            con.Close();
            return Ok(blogdata);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUpdate(int id, BlogViewModel blog)
        {
            SqlConnection con = new SqlConnection(_connection);
            
            string filter = string.Empty;
            con.Open();
            if(blog.Title is not null)
            {
                filter += " [BlogTitle] = @blogTitle, ";
            }
            if (blog.Author is not null) 
            {
                filter += " [BlogAuthor] = @blogAuthor, ";
            }
            if (blog.Content is not null)
            {
                filter += " [BlogContent] = @blogContent, ";
            }
            if(filter.Length == 0 || id < 0)
            {
                return BadRequest("Invalid Request");
            }

            filter = filter.Substring(0, filter.Length - 2);

            string updquery = $@"UPDATE [dbo].[Tbl_Blog]
                                SET {filter}
                                WHERE [BlogId] = @blogId";

            SqlCommand cmd = new SqlCommand(updquery, con);
            if (!string.IsNullOrEmpty(blog.Title)) cmd.Parameters.AddWithValue("@blogTitle", blog.Title);
            if (!string.IsNullOrEmpty(blog.Author)) cmd.Parameters.AddWithValue("@blogAuthor", blog.Author);
            if (!string.IsNullOrEmpty(blog.Content)) cmd.Parameters.AddWithValue("@blogContent", blog.Content);
            cmd.Parameters.AddWithValue("@blogId", id);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            if (result == 1)
            {
                return Ok(blog);
            }
            else
            {
                return BadRequest("No row affected");
            }
        }
    }
}

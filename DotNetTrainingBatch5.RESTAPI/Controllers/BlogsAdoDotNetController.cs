using DotNetTrainingBatch5.RESTAPI.Data_Model;
using DotNetTrainingBatch5.RESTAPI.View_Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Immutable;

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

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            List<BlogViewModel> list = new List<BlogViewModel>();

            SqlConnection con = new SqlConnection(_connection);
            con.Open();
            string query = "SELECT * FROM TBL_BLOG WHERE BlogId=@id";
            SqlCommand cmd = new SqlCommand(@query, con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                list.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    Flag = 0
                });
            }
            con.Close();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Create(BlogViewModel blog)
        {
            SqlConnection con = new SqlConnection(_connection);
            con.Open();
            string filter = string.Empty;

            if (!string.IsNullOrEmpty(blog.Title))
            {
                filter += " @blogtitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                filter += " @blogauthor, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                filter += " @blogcontent, ";
            }

            if(filter.Length == 0 || filter.Length < 0)
            {
                return BadRequest();
            }

            filter = filter.Substring(0, filter.Length - 2);

            string query = $@"insert into [dbo].[tbl_blog]
               ([blogtitle]
               ,[blogauthor]
               ,[blogcontent]
               ,[deleteflag])
            values
               (
                 {filter},0
               )";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@blogtitle", blog.Title);
            cmd.Parameters.AddWithValue("@blogauthor", blog.Author);
            cmd.Parameters.AddWithValue("@blogcontent", blog.Content);
            int result = cmd.ExecuteNonQuery();
            
            con.Close();
            if (result == 1)
            {
                return Ok("Insert Sucuess");
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogViewModel blog)
        {
            SqlConnection con = new SqlConnection(_connection);
            string filter = string.Empty;

            if(blog.Title != null)
            {
                filter += " BlogTitle = @blogtitle, ";
            }
            if (blog.Author != null)
            {
                filter += " BlogAuthor = @blogauthor, ";
            }
            if (blog.Content != null)
            {
                filter += " BlogContent = @blogcontent, ";
            }

            filter = filter.Substring(0, filter.Length - 2);

            con.Open();
            string updquery = $@"UPDATE [dbo].[Tbl_Blog]
                                SET {filter}
                                WHERE [BlogId] = @blogId";

            SqlCommand cmd = new SqlCommand(updquery, con);
            cmd.Parameters.AddWithValue("@blogId", id);
            cmd.Parameters.AddWithValue("@blogtitle", blog.Title);
            cmd.Parameters.AddWithValue("@blogauthor", blog.Author);
            cmd.Parameters.AddWithValue("@blogcontent", blog.Content);
            int res = cmd.ExecuteNonQuery();
            con.Close();

            if (res > 0)
            {
                return Ok("Success Update");
            }
            else { return BadRequest(); }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection con = new SqlConnection(_connection);
            con.Open();
            string query = "Delete from Tbl_Blog where BlogId=@blogid";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@blogid", id);
            int res = cmd.ExecuteNonQuery();
            con.Close();
            if (res > 0)
            {
                return Ok("Delete Success");
            }
            else
            {
                return BadRequest("Delete Failed");
            }
        }
    }
}

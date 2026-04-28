using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoClient.Services
{
    public interface IBlogApi
    {
        [Get("/api/Blogs")]
        Task<List<ResTblBlog>> GetBlogs();

        [Get("/api/Blogs/{id}")]
        Task<List<ResTblBlog>> GetBlog(int id);

        [Post("/api/Blogs")]
        Task<ResTblBlog> CreateBlog(ResTblBlog tblBlog);
    }

    public class ResTblBlog
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string? BlogContent { get; set; }

        public bool? DeleteFlag { get; set; }
    }
}

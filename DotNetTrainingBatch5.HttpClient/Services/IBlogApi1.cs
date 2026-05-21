using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoClient.Services
{
    public interface IBlogApi1
    {
        [Get("/api/Blogs")]
        Task<List<refitBlog>> GetBlogs();

        [Get("/api/Blogs/{id}")]
        Task<List<refitBlog>> GetBlogid(int id);
    }

    public partial class refitBlog
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string? BlogContent { get; set; }

        public bool? DeleteFlag { get; set; }
    }
}

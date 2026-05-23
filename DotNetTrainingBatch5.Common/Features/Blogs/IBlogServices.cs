using DotNetTrainingBatch5.Database.Models;

namespace DotNetTrainingBatch5.Common.Features.Blogs
{
    public interface IBlogServices
    {
        TblBlog createBlog(TblBlog data_blog);
        TblBlog? deleteBlog(int id);
        List<TblBlog> getBlog(int id);
        List<TblBlog> getBlogs();
        TblBlog patchBlog(int id, TblBlog data_blog);
        TblBlog updateBlog(int id, TblBlog data_blog);
    }
}
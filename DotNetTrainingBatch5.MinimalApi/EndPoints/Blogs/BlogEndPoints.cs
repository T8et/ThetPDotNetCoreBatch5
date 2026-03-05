using DotNetTrainingBatch5.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.MinimalApi.EndPoints.Blogs
{
    public static class BlogEndPoints
    {
        //public static string hello(this string nm)
        //{
        //    return "hello" + nm;
        //}

        public static void BlogEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", () =>
            {
                AppDBContext db = new AppDBContext();
                var blog_data = db.TblBlogs.AsNoTracking().ToList();
                return Results.Ok(blog_data);
            })
                .WithName("GetBlogs")
                .WithOpenApi();

            app.MapGet("/blog/{id}", (int id) =>
            {
                AppDBContext db = new AppDBContext();
                var result = db.TblBlogs.AsNoTracking().Where(x => x.BlogId == id).ToList();
                if (result is null) return Results.BadRequest("Not Found");

                return Results.Ok(result);
            })
                .WithName("GetBlogId")
                .WithOpenApi();

            app.MapPost("/blog", (TblBlog blog) =>
            {
                AppDBContext db = new AppDBContext();
                db.TblBlogs.Add(blog);
                db.SaveChanges();
                return Results.Ok(blog);
            })
                .WithName("PostBlog")
                .WithOpenApi();

            app.MapPut("/blog/{id}", (int id, TblBlog blog) =>
            {
                AppDBContext db = new AppDBContext();
                var item = db.TblBlogs.AsNoTracking().Where(x => x.BlogId == id).FirstOrDefault();
                if (item is null) return Results.BadRequest("Not Found");

                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok("Successful Update");
            })
                .WithName("PutBlog")
                .WithOpenApi();

            app.MapDelete("/blog/{id}", (int id) =>
            {
                AppDBContext db = new AppDBContext();
                var result = db.TblBlogs.AsNoTracking().Where(x => x.BlogId == id).FirstOrDefault();

                if (result is null) return Results.BadRequest("Data Not Found!");

                db.TblBlogs.Remove(result);

                db.Entry(result).State = EntityState.Deleted;
                db.SaveChanges();
                return Results.Ok("Delete Success");
            })
                .WithName("DeleteBlogs")
                .WithOpenApi();
        }
    }
}

using DotNetTrainingBatch5.Common.Features.Blogs;
using DotNetTrainingBatch5.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.MinimalApi.EndPoints.Blogs
{
    public static class BlogServicesEndpoints
    {
        //public static string hello(this string nm)
        //{
        //    return "hello" + nm;
        //}

        public static void BlogServicesEndpoint(this IEndpointRouteBuilder app)
        {

            BlogServices _service = new BlogServices();

            app.MapGet("/blogs", () =>
            {
                var data = _service.getBlogs();
                return Results.Ok(data);
            })
                .WithName("GetBlogs")
                .WithOpenApi();

            app.MapGet("/blog/{id}", (int id) =>
            {               
                var result = _service.getBlog(id);
                if (result is null) return Results.BadRequest("Not Found");

                return Results.Ok(result);
            })
                .WithName("GetBlogId")
                .WithOpenApi();

            app.MapPost("/blog", (TblBlog blog) =>
            {
                var data = _service.createBlog(blog);
                return Results.Ok(blog);
            })
                .WithName("PostBlog")
                .WithOpenApi();

            app.MapPut("/blog/{id}", (int id, TblBlog blog) =>
            {
                var result = _service.updateBlog(id, blog);
                return Results.Ok("Successful Update");
            })
                .WithName("PutBlog")
                .WithOpenApi();

            app.MapDelete("/blog/{id}", (int id) =>
            {
                var result = _service.deleteBlog(id);
                return Results.Ok("Delete Success");
            })
                .WithName("DeleteBlogs")
                .WithOpenApi();
        }
    }
}

using DotNetTrainingBatch5.Database.Models;
using DotNetTrainingBatch5.MinimalApi.EndPoints.Blogs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

//app.MapGet("/blogs", () =>
//{
//    AppDBContext db = new AppDBContext();
//    var blog_data = db.TblBlogs.AsNoTracking().ToList();
//    return Results.Ok(blog_data);
//})
//    .WithName("GetBlogs")
//    .WithOpenApi();

//app.MapGet("/blog/{id}", (int id) =>
//{
//    AppDBContext db = new AppDBContext();
//    var result = db.TblBlogs.AsNoTracking().Where(x=>x.BlogId == id).ToList();
//    if(result is null) return Results.BadRequest("Not Found");

//    return Results.Ok(result);
//})
//    .WithName("GetBlogId")
//    .WithOpenApi();

//app.MapPost("/blog", (TblBlog blog) =>
//{
//    AppDBContext db = new AppDBContext();
//    db.TblBlogs.Add(blog);
//    db.SaveChanges();
//    return Results.Ok(blog);
//})
//    .WithName("PostBlog")
//    .WithOpenApi();

//app.MapPut("/blog/{id}", (int id, TblBlog blog) =>
//{
//    AppDBContext db = new AppDBContext();
//    var item = db.TblBlogs.AsNoTracking().Where(x => x.BlogId == id).FirstOrDefault();
//    if (item is null) return Results.BadRequest("Not Found");

//    item.BlogTitle = blog.BlogTitle;
//    item.BlogAuthor  = blog.BlogAuthor;
//    item.BlogContent = blog.BlogContent;

//    db.Entry(item).State = EntityState.Modified;
//    db.SaveChanges();
//    return Results.Ok("Successful Update");
//})
//    .WithName("PutBlog")
//    .WithOpenApi();

//app.MapDelete("/blog/{id}", (int id) =>
//{
//    AppDBContext db = new AppDBContext();
//    var result = db.TblBlogs.AsNoTracking().Where(x=>x.BlogId == id).FirstOrDefault();

//    if (result is null) return Results.BadRequest("Data Not Found!");

//    db.TblBlogs.Remove(result);

//    db.Entry(result).State = EntityState.Deleted;
//    db.SaveChanges();
//    return Results.Ok("Delete Success");
//})
//    .WithName("DeleteBlogs")
//    .WithOpenApi();

//BlogEndPoints.hello("Kempo");

//"kempo".hello();

//app.BlogEndPoint();
app.BlogServicesEndpoint();

app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//   public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}

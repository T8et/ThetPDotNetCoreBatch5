using DotNetTrainingBatch5.Common.Features.Blogs;
using DotNetTrainingBatch5.Database.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection
//UI
//BL
//DA
builder.Services.AddDbContext<AppDBContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});
//builder.Services.AddScoped<IBlogServices, BlogServices>();
builder.Services.AddScoped<IBlogServices, BlogServicesV1>();


// 1. Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. DEFINE THE CORS POLICY (Add this block)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyFrontend",
        policy =>
        {
            // Put your exact frontend IP and port 8080 here
            policy.WithOrigins("http://192.168.100.9:8080")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// 3. ENABLE THE CORS POLICY (Add this line)
// CRITICAL: This MUST be placed right here—after UseRouting (if you have it) 
// and BEFORE UseAuthorization / MapControllers.
app.UseCors("AllowMyFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
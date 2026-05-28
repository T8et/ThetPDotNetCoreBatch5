using Microsoft.AspNetCore.Mvc;
using Refit;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(n => new HttpClient()
{
    BaseAddress = new Uri(builder.Configuration.GetSection("ApiLink").Value!)
});

builder.Services.AddSingleton(n => new RestClient(builder.Configuration.GetSection("ApiLink").Value!));

builder.Services
    .AddRefitClient<IsnakeApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiLink").Value!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/birds", async ([FromServices] HttpClient httpclient) =>
{
    var response = await httpclient.GetAsync("birds");
    return await response.Content.ReadAsStringAsync();
});

app.MapGet("/pickpile", async ([FromServices] RestClient resCli) =>
{
    RestRequest request = new RestRequest("pick-a-pile", Method.Get);
    var response = await resCli.GetAsync(request);
    return response.Content;
});

app.MapGet("/snakes", async ([FromServices] IsnakeApi snakeApi) =>
{
    var response = await snakeApi.GetSnakes();
    return response;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public interface IsnakeApi
{
    [Get("/snakes")]
    Task<List<SnakeModel>> GetSnakes();
}


public class SnakeModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string MMName { get; set; }
    public string EngName { get; set; }
    public string Detail { get; set; }
    public string IsPoison { get; set; }
    public string IsDanger { get; set; }
}
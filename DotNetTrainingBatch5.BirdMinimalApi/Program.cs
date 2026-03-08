using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/birds", () =>
{
    string folder = "Data/Birds.json";
    var jsondata = File.ReadAllText(folder);
    var rest = JsonConvert.DeserializeObject<BirdData>(jsondata)!;
    return Results.Ok(rest.Tbl_Bird);
}).WithName("Birds").WithOpenApi();

app.MapGet("/birds/{id}", (int id) =>
{
    string folder = "Data/Birds.json";
    var jsondata = File.ReadAllText(folder);

    var rest = JsonConvert.DeserializeObject<BirdData>(jsondata)!;
    var item = rest.Tbl_Bird.FirstOrDefault(x => x.Id == id);

    if (item is null) return Results.BadRequest();

    return Results.Ok(item);

}).WithName("BirdsWithID").WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


public class BirdData
{
    public Tbl_Bird[] Tbl_Bird { get; set; }
}

public class Tbl_Bird
{
    public int Id { get; set; }
    public string? BirdMyanmarName { get; set; }
    public string? BirdEnglishName { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
}

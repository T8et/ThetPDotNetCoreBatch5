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

app.MapPost("/birdCreate", (Tbl_Bird requestObj) =>
{
    string folder = "Data/Birds.json";
    var jsondata = File.ReadAllText(folder);
    var retobj = JsonConvert.DeserializeObject<BirdData>(jsondata)!;

    requestObj.Id = retobj.Tbl_Bird.Count() == 0 ? 1 : retobj.Tbl_Bird.Max(x => x.Id) + 1;
    retobj.Tbl_Bird.Add(requestObj);

    var jsondt = JsonConvert.SerializeObject(retobj);
    File.WriteAllText(folder, jsondt);

    return Results.Ok(jsondt);
}).WithName("BirdsCreate").WithOpenApi();


app.MapPut("/birdUpdate/{id}", (int uid, Tbl_Bird updateData) =>
{
    string folder = "Data/Birds.json";
    var readtxt = File.ReadAllText(folder);
    var retObj = JsonConvert.DeserializeObject<BirdData>(readtxt);

    var item = retObj.Tbl_Bird.Where(x=>x.Id==uid).FirstOrDefault();

    if (item is null) return Results.BadRequest();

    updateData.Id = item.Id;
    item.Id = updateData.Id;
    item.BirdMyanmarName = updateData.BirdMyanmarName;
    item.BirdEnglishName = updateData.BirdEnglishName;
    item.Description = updateData.Description;
    item.ImagePath = updateData.ImagePath;

    //retObj.Tbl_Bird.Add(item);

    var jsondt = JsonConvert.SerializeObject(retObj);
    File.WriteAllText(folder, jsondt);

    return Results.Ok(jsondt);

}).WithName("BirdUpdate").WithOpenApi();


app.MapDelete("/birdDel/{id}", (int did) =>
{
    string folder = "Data/Birds.json";
    var txt = File.ReadAllText(folder);
    var retobj = JsonConvert.DeserializeObject<BirdData>(txt);

    var item = retobj.Tbl_Bird.Where(x=>x.Id == did).FirstOrDefault();
    if (item is null) return Results.BadRequest();

    retobj.Tbl_Bird.Remove(item);

    var item1 = JsonConvert.SerializeObject(retobj);
    File.WriteAllText(folder, item1);

    return Results.Ok("Deleted");
}).WithName("BirdDelete").WithOpenApi();


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
    public List<Tbl_Bird> Tbl_Bird { get; set; }
}

public class Tbl_Bird
{
    public int Id { get; set; }
    public string? BirdMyanmarName { get; set; }
    public string? BirdEnglishName { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }
}

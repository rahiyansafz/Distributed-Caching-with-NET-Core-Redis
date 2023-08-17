using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = "127.0.0.1:6379";
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", ([FromServices] IDistributedCache cache) =>
{
    return TypedResults.Ok(cache.GetString("dessert"));
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

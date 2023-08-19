using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

using RedisWebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(config =>
{
    config.Configuration = builder.Environment.IsDevelopment()
        ? "127.0.0.1:6379"
        : Environment.GetEnvironmentVariable("REDIS_URL");
});

builder.Services.AddTransient<ServiceOne>()
    .AddTransient<ServiceTwo>()
    .AddTransient<ServiceThree>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/one", async (IDistributedCache _cache, [FromServices] ServiceOne one, [FromQuery] int n) =>
{
    var res = await _cache.GetOrAddAsync(n, one.GetCarAsync); // n = 1

    return res;
})
.WithName("One")
.WithOpenApi();

app.MapGet("/two", async (IDistributedCache _cache, [FromServices] ServiceTwo two, [FromQuery] string key) =>
{
    var res = await _cache.GetOrAddAsync(key, two.GetNameAsync); // key = "foo"

    return res;
})
.WithName("Two")
.WithOpenApi();

app.MapGet("/three", async (IDistributedCache _cache, [FromServices] ServiceThree three) =>
{
    Dude dude = new(1, "Pizza");
    await _cache.SetAsync(dude);
    await three.SaveDudeAsync(dude);
    return Results.Ok();
})
.WithName("Three")
.WithOpenApi();

app.Run();
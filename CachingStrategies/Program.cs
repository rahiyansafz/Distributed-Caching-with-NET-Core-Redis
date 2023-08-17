using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;

var options = Options.Create(new RedisCacheOptions()
{
    Configuration = "127.0.0.1:6379"
});
IDistributedCache _cache = new RedisCache(options);

_cache.SetString("dessert", "ice cream"); // HGET dessert data
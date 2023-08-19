using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;

namespace RedisConfig;
public class DistributedCacheTtl
{
    readonly IDistributedCache _cache = new RedisCache(
        Options.Create(new RedisCacheOptions()
        {
            Configuration = "127.0.0.1:6379"
        })
    );

    public void Run()
    {
        //AbsoluteExpiration();

        // SlidingExpiration();

        _cache.GetString("key");
    }

    public void AbsoluteExpiration()
    {
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
        };

        _cache.SetString("key", "string", options);
    }

    public void SlidingExpiration()
    {
        var options = new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromSeconds(30)
        };

        _cache.SetString("key", "string", options);
    }
}

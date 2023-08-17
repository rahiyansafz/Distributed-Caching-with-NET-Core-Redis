using Microsoft.Extensions.Caching.Distributed;

namespace CachingStrategies;
public class WriteThroughCache : IDistributedCache
{
    private readonly IDistributedCache _main;
    private readonly IDistributedCache _secondary;

    public WriteThroughCache(IDistributedCache main, IDistributedCache secondary)
    {
        _main = main;
        _secondary = secondary;
    }

    // read aside
    public byte[]? Get(string key)
    {
        var value = _secondary.Get(key);
        if (value is null)
        {
            value = _main.Get(key);
            _secondary.Set(key, value!);
        }

        return value;
    }

    public Task<byte[]?> GetAsync(string key, CancellationToken token = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        _secondary.Set(key, value);
        _main.Set(key, value);
    }

    public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public void Refresh(string key)
    {
        throw new NotImplementedException();
    }

    public Task RefreshAsync(string key, CancellationToken token = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public void Remove(string key)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(string key, CancellationToken token = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}
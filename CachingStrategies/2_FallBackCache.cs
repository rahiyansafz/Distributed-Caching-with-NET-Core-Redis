using Microsoft.Extensions.Caching.Distributed;

namespace CachingStrategies;
public class FallBackCache : IDistributedCache
{
    private readonly IDistributedCache _main;
    private readonly IDistributedCache _secondary;

    private int _failCount;

    public FallBackCache(IDistributedCache main, IDistributedCache secondary)
    {
        _main = main;
        _secondary = secondary;
    }

    public byte[]? Get(string key)
    {
        try
        {
            if (_failCount < 3)
                _main.Get(key);
        }
        catch (Exception)
        {
            _failCount++;
        }

        return _secondary.Get(key)!;
    }

    public Task<byte[]?> GetAsync(string key, CancellationToken token = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        throw new NotImplementedException();
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
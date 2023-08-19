using Microsoft.Extensions.Caching.Distributed;

namespace CachingStrategies;
public class WriteBackCache : IDistributedCache
{
    private readonly IDistributedCache _main;
    private readonly IDistributedCache _secondary;

    public WriteBackCache(IDistributedCache main, IDistributedCache secondary)
    {
        _main = main;
        _secondary = secondary;
        _backgroundTask = Task.Run(WriteBack);
    }

    public byte[]? Get(string key)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]?> GetAsync(string key, CancellationToken token = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    private readonly List<KeyValuePair<string, byte[]>> _writeBackBuffer = new();
    private readonly Task _backgroundTask;

    private async Task WriteBack()
    {
        while (true)
        {
            try
            {
                if (_writeBackBuffer.Count > 100)
                {
                    // build batch update
                }

                await Task.Delay(1000 * 60);
            }
            catch (Exception)
            {

            }
        }
    }

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        _secondary.Set(key, value);
        _writeBackBuffer.Add(KeyValuePair.Create(key, value));
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
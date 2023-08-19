namespace RedisWebApp;

public class ServiceTwo
{
    public Task<string> GetNameAsync(string id)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        return Task.FromResult("Pizza");
    }
}
using Microsoft.Extensions.Caching.Distributed;
using MovieApp.MovieApi.Domain.Interfaces.Services;
using Newtonsoft.Json;

namespace MovieApp.MovieApi.Infra.Cache;
public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;

    public CacheService(IDistributedCache cache, DistributedCacheEntryOptions options)
    {
        _cache = cache;
        _options = options;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var cachedValue = await _cache.GetStringAsync(key);
        if (!string.IsNullOrEmpty(cachedValue))
        {
            return JsonConvert.DeserializeObject<T>(cachedValue);
        }
        return default;
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var serializedValue = JsonConvert.SerializeObject(value);
        await _cache.SetStringAsync(key, serializedValue, _options);
    }
}

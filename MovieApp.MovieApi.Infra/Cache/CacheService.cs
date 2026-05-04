using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MovieApp.MovieApi.Domain.Interfaces.Services;
using Newtonsoft.Json;

namespace MovieApp.MovieApi.Infra.Cache;
public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;
    private readonly ILogger<CacheService> _logger;

    public CacheService(IDistributedCache cache, DistributedCacheEntryOptions options, ILogger<CacheService> logger)
    {
        _cache = cache;
        _options = options;
        _logger = logger;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        try
        {
            var cachedValue = await _cache.GetStringAsync(key);

            if (string.IsNullOrWhiteSpace(cachedValue))
                return default;

            return JsonConvert.DeserializeObject<T>(cachedValue);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Cache GET failed for key {CacheKey}", key);
            return default;
        }
    }

    public async Task RemoveAsync(string key)
    {
        try
        {
            await _cache.RemoveAsync(key);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Cache REMOVE failed for key {CacheKey}", key);
        }
    }

    public async Task SetAsync<T>(string key, T value)
    {
        try
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            await _cache.SetStringAsync(key, serializedValue, _options);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Cache SET failed for key {CacheKey}", key);
        }
    }
}

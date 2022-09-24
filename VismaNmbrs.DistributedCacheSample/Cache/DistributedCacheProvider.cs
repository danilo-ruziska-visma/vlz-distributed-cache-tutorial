using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace VismaNmbrs.DistributedCacheSample.Cache
{
    public class DistributedCacheProvider : ICacheProvider
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedCacheProvider(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetOrCreateFromCache<T>(string key, TimeSpan slidingExpiration, Func<Task<T>> getObjectFromOriginalSource) where T : class
        {
            var cachedResponse = await _distributedCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(cachedResponse))
            {
                var response = await getObjectFromOriginalSource();
                await SetCache(key, response, slidingExpiration);
                return response;
            }
            else
            {
                return JsonSerializer.Deserialize<T>(cachedResponse);
            }
        }

        public async Task SetCache<T>(string key, T value, TimeSpan slidingExpiration) where T : class
        {
            var valueSerialized = JsonSerializer.Serialize(value);
            await _distributedCache.SetStringAsync(key, valueSerialized, new DistributedCacheEntryOptions()
            {
                SlidingExpiration = slidingExpiration,
            });
        }
    }
}

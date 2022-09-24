using Microsoft.Extensions.Caching.Memory;

namespace VismaNmbrs.DistributedCacheSample.Cache
{
    public class LocalCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public LocalCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T?> GetOrCreateFromCache<T>(string key, TimeSpan slidingExpiration, Func<Task<T>> getObjectFromOriginalSource) where T : class
        {
            var cacheEntry = await _memoryCache.GetOrCreateAsync<T?>(key, async entry =>
            {
                entry.SlidingExpiration = slidingExpiration;
                return await getObjectFromOriginalSource();
            });
            return cacheEntry;
        }

        public Task SetCache<T>(string key, T value, TimeSpan slidingExpiration) where T : class
        {
            return Task.FromResult(_memoryCache.Set(key, value, slidingExpiration));
        }
    }
}

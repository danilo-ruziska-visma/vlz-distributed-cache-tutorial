using Microsoft.Extensions.Caching.Distributed;

namespace VismaNmbrs.DistributedCacheSample.Cache
{
    public interface ICacheProvider
    {
        Task<T?> GetOrCreateFromCache<T>(string key, TimeSpan slidingExpiration, Func<Task<T>> getObjectFromOriginalSource) where T : class;
        Task SetCache<T>(string key, T value, TimeSpan slidingExpiration) where T : class;
    }
}

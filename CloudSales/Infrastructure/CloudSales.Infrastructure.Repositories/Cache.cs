namespace CloudSales.Infrastructure.Repositories
{
    public class Cache : ICache
    {
        public Task<CacheResult<T>> GetFromCacheAsync<T>(string cacheKey) where T : class 
        {
            // TODO: Implement reading from actual cache, e.g. using Redis
            return Task.FromResult(new CacheResult<T>() { IsCacheHit = false, Result = null });
        }

        public Task SaveToCacheAsync<T>(string cacheKey, T? value, int cacheForSeconds) where T : class
        {
            // TODO: Implement actual caching, e.g. using Redis
            return Task.CompletedTask;
        }
    }
}

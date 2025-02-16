namespace CloudSales.Infrastructure.Repositories
{
    public interface ICache
    {
        Task<CacheResult<T>> GetFromCacheAsync<T>(string cacheKey) where T : class;
        Task SaveToCacheAsync<T>(string cacheKey, T? value, int cacheForSeconds) where T : class;
    }
}

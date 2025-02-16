using CloudSales.Domain.Models;
using CloudSales.Domain.Repositories;
using CloudSales.Infrastructure.Ccp;

namespace CloudSales.Infrastructure.Repositories
{
    public class SoftwareRepository : ISoftwareRepository
    {
        private readonly ICcpClient _ccpClient;
        private readonly ICache _cache;

        public SoftwareRepository(ICcpClient ccpClient, ICache cache)
        {
            _ccpClient = ccpClient;
            _cache = cache;
        }

        private const int CacheForSeconds = 3600;

        public async Task<AvailableSoftware?> GetAvailableSoftwareByIdAsync(Guid id)
        {
            var cacheKey = GenerateSoftwareCacheKey(id);
            var cached = await _cache.GetFromCacheAsync<AvailableSoftware>(cacheKey);

            if (cached.IsCacheHit)
            {
                return cached.Result;
            }

            var software = await _ccpClient.GetAvailableSoftwareByIdAsync(id);

            await _cache.SaveToCacheAsync(cacheKey, software, CacheForSeconds);

            return software;
        }

        public async Task<IEnumerable<AvailableSoftware>> GetAvailableSoftwaresAsync(int page)
        {
            var cacheKey = GenerateSoftwarePageCacheKey(page);
            var cached = await _cache.GetFromCacheAsync<IEnumerable<AvailableSoftware>>(cacheKey);

            if (cached.IsCacheHit)
            {
                return cached.Result ?? [];
            }

            var softwares = await _ccpClient.GetAvailableSoftwaresAsync(page);

            await _cache.SaveToCacheAsync(cacheKey, softwares, CacheForSeconds);

            return softwares;
        }

        public Task<OrderResult> OrderSoftwareAsync(Order order)
        {
            return _ccpClient.OrderSoftwareAsync(order);
        }

        private static string GenerateSoftwareCacheKey(Guid id) => $"Cache:Software:{id}";

        private static string GenerateSoftwarePageCacheKey(int page) => $"Cache:SoftwarePage:{page}";
    }
}

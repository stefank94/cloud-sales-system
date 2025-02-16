namespace CloudSales.Infrastructure.Repositories
{
    public class CacheResult<T> where T : class
    {
        public bool IsCacheHit { get; init; }
        public T? Result { get; init; }
    }
}

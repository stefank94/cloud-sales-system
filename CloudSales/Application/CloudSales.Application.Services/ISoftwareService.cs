using CloudSales.Domain.Models;

namespace CloudSales.Application.Services
{
    public interface ISoftwareService
    {
        Task<IEnumerable<AvailableSoftware>> GetAvailableSoftwaresAsync(int page);
        Task<AvailableSoftware?> GetAvailableSoftwareByIdAsync(Guid id);
        Task<IEnumerable<PurchasedSoftware>> GetPurchasedSoftwareForAccountAsync(Guid accountId, int page);
        Task<PurchasedSoftware> OrderSoftwareAsync(Order order);
    }
}

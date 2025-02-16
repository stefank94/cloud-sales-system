using CloudSales.Domain.Models;

namespace CloudSales.Domain.Repositories
{
    public interface IPurchasedSoftwareRepository
    {
        Task<IEnumerable<PurchasedSoftware>> GetPurchasedSoftwareForAccountAsync(Guid accountId, int page);

        Task InsertPurchasedSoftwareAsync(PurchasedSoftware purchasedSoftware);
    }
}

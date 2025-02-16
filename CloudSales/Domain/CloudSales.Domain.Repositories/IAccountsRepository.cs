using CloudSales.Domain.Models;

namespace CloudSales.Domain.Repositories
{
    public interface IAccountsRepository
    {
        Task<IEnumerable<Account>> GetAccountsForCustomerAsync(Guid customerId, int page);
        Task<Account?> GetAccountByIdAsync(Guid accountId);
    }
}

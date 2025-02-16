using CloudSales.Domain.Models;

namespace CloudSales.Application.Services
{
    public interface IAccountsService
    {
        Task<IEnumerable<Account>> GetAccountsForCustomerAsync(Guid customerId, int page);
    }
}

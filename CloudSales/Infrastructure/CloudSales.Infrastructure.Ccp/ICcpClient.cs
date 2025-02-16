using CloudSales.Domain.Models;

namespace CloudSales.Infrastructure.Ccp
{
    public interface ICcpClient
    {
        Task<AvailableSoftware?> GetAvailableSoftwareByIdAsync(Guid id);
        Task<IEnumerable<AvailableSoftware>> GetAvailableSoftwaresAsync(int page);
        Task<OrderResult> OrderSoftwareAsync(Order order);
    }
}

using CloudSales.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSales.Domain.Repositories
{
    public interface ISoftwareRepository
    {
        Task<AvailableSoftware?> GetAvailableSoftwareByIdAsync(Guid id);
        Task<IEnumerable<AvailableSoftware>> GetAvailableSoftwaresAsync(int page);
        Task<OrderResult> OrderSoftwareAsync(Order order);
    }
}

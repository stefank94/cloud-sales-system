using CloudSales.Domain.Models;
using CloudSales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSales.Application.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsService(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<IEnumerable<Account>> GetAccountsForCustomerAsync(Guid customerId, int page)
        {
            return await _accountsRepository.GetAccountsForCustomerAsync(customerId, page);
        }
    }
}

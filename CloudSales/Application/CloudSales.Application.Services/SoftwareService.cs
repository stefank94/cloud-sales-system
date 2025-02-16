using CloudSales.Domain.Exceptions;
using CloudSales.Domain.Models;
using CloudSales.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace CloudSales.Application.Services
{
    public class SoftwareService : ISoftwareService
    {
        private readonly IPurchasedSoftwareRepository _purchasedSoftwareRepository;
        private readonly IAccountsRepository _accountsRepository;
        private readonly ISoftwareRepository _softwareRepository;
        private readonly ILogger<SoftwareService> _logger;

        public SoftwareService(IPurchasedSoftwareRepository purchasedSoftwareRepository, IAccountsRepository accountsRepository, ISoftwareRepository softwareRepository, ILogger<SoftwareService> logger)
        {
            _purchasedSoftwareRepository = purchasedSoftwareRepository;
            _accountsRepository = accountsRepository;
            _softwareRepository = softwareRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<PurchasedSoftware>> GetPurchasedSoftwareForAccountAsync(Guid accountId, int page)
        {
            return await _purchasedSoftwareRepository.GetPurchasedSoftwareForAccountAsync(accountId, page);
        }

        public async Task<PurchasedSoftware> OrderSoftwareAsync(Order order)
        {
            if (order.ValidTo < DateTime.UtcNow)
            {
                throw new ValidToDateInPastException();
            }

            var accountTask = _accountsRepository.GetAccountByIdAsync(order.AccountId);
            var softwareTask = GetAvailableSoftwareByIdAsync(order.SoftwareId);

            await Task.WhenAll(accountTask, softwareTask);

            if (accountTask.Result is null)
            {
                throw new AccountNotFoundException(order.AccountId);
            }

            if (softwareTask.Result is null)
            {
                throw new SoftwareNotFoundException(order.SoftwareId);
            }

            var purchasedOrder = await _softwareRepository.OrderSoftwareAsync(order);
            if (!purchasedOrder.IsSuccessful)
            {
                _logger.LogError("Could not purchase software {SoftwareId} for account {AccountId}", order.SoftwareId, order.AccountId);
                throw new OrderNotSuccessfulException();
            }

            _logger.LogInformation("Purchased software {SoftwareId} for account {AccountId}", order.SoftwareId, order.AccountId);

            var purchasedSoftware = new PurchasedSoftware()
            {
                AccountId = order.AccountId,
                Id = purchasedOrder.OrderId,
                Quantity = order.Quantity,
                Name = softwareTask.Result.Name,
                SoftwareId = order.SoftwareId,
                ValidTo = order.ValidTo
            };

            await _purchasedSoftwareRepository.InsertPurchasedSoftwareAsync(purchasedSoftware);

            return purchasedSoftware;
        }

        public Task<AvailableSoftware?> GetAvailableSoftwareByIdAsync(Guid id) => _softwareRepository.GetAvailableSoftwareByIdAsync(id);

        public Task<IEnumerable<AvailableSoftware>> GetAvailableSoftwaresAsync(int page) => _softwareRepository.GetAvailableSoftwaresAsync(page);
            
    }
}

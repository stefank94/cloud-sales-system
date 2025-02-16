using CloudSales.Application.Services;
using CloudSales.Domain.Exceptions;
using CloudSales.Domain.Models;
using CloudSales.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSales.Tests.Application.Services
{
    public class SoftwareServiceTests
    {
        private readonly SoftwareService _softwareService;
        private readonly IPurchasedSoftwareRepository _purchasedSoftwareRepository = Substitute.For<IPurchasedSoftwareRepository>();
        private readonly IAccountsRepository _accountsRepository = Substitute.For<IAccountsRepository>();
        private readonly ISoftwareRepository _softwareRepository = Substitute.For<ISoftwareRepository>();

        public SoftwareServiceTests()
        {
            _softwareService = new SoftwareService(_purchasedSoftwareRepository, _accountsRepository, _softwareRepository, Substitute.For<ILogger<SoftwareService>>());
        }

        [Fact]
        public async Task OrderSoftwareAsync_InvalidDate_ThrowsValidToDateInPastException()
        {
            var order = GetSampleOrder();
            order.ValidTo = DateTime.UtcNow.AddDays(-1);

            await Assert.ThrowsAsync<ValidToDateInPastException>(() => _softwareService.OrderSoftwareAsync(order));
        }

        [Fact]
        public async Task OrderSoftwareAsync_AccountNotFound_ThrowsAccountNotFoundException()
        {
            var order = GetSampleOrder();

            var software = new AvailableSoftware()
            {
                Id = order.SoftwareId,
                Name = "Microsoft Office"
            };

            _accountsRepository.GetAccountByIdAsync(order.AccountId).ReturnsNull();
            _softwareRepository.GetAvailableSoftwareByIdAsync(order.SoftwareId).Returns(software); 

            await Assert.ThrowsAsync<AccountNotFoundException>(() => _softwareService.OrderSoftwareAsync(order));
        }

        [Fact]
        public async Task OrderSoftwareAsync_SoftwareNotFound_ThrowsSoftwareNotFoundException()
        {
            var order = GetSampleOrder();

            var account = new Account()
            {
                Id = order.AccountId,
                Name = "Account 1"
            };

            _accountsRepository.GetAccountByIdAsync(order.AccountId).Returns(account);
            _softwareRepository.GetAvailableSoftwareByIdAsync(order.SoftwareId).ReturnsNull();

            await Assert.ThrowsAsync<SoftwareNotFoundException>(() => _softwareService.OrderSoftwareAsync(order));
        }

        [Fact]
        public async Task OrderSoftwareAsync_OrderNotSuccessful_ThrowsOrderNotSuccessfulException()
        {
            var order = GetSampleOrder();

            var software = new AvailableSoftware()
            {
                Id = order.SoftwareId,
                Name = "Microsoft Office"
            };
            var account = new Account()
            {
                Id = order.AccountId,
                Name = "Account 1"
            };
            var orderResult = new OrderResult()
            {
                IsSuccessful = false
            };

            _accountsRepository.GetAccountByIdAsync(order.AccountId).Returns(account);
            _softwareRepository.GetAvailableSoftwareByIdAsync(order.SoftwareId).Returns(software);
            _softwareRepository.OrderSoftwareAsync(order).Returns(orderResult);

            await Assert.ThrowsAsync<OrderNotSuccessfulException>(() => _softwareService.OrderSoftwareAsync(order));
        }

        [Fact]
        public async Task OrderSoftwareAsync_OrderSuccessful_ReturnsPurchasedSoftware()
        {
            var order = GetSampleOrder();

            var software = new AvailableSoftware()
            {
                Id = order.SoftwareId,
                Name = "Microsoft Office"
            };
            var account = new Account()
            {
                Id = order.AccountId,
                Name = "Account 1"
            };
            var orderResult = new OrderResult()
            {
                IsSuccessful = true,
                OrderId = Guid.NewGuid()
            };

            _accountsRepository.GetAccountByIdAsync(order.AccountId).Returns(account);
            _softwareRepository.GetAvailableSoftwareByIdAsync(order.SoftwareId).Returns(software);
            _softwareRepository.OrderSoftwareAsync(order).Returns(orderResult);

            var result = await _softwareService.OrderSoftwareAsync(order);
            
            Assert.NotNull(result);
            Assert.Equal(orderResult.OrderId, result.Id);
            Assert.Equal(order.AccountId, result.AccountId);
            Assert.Equal(order.SoftwareId, result.SoftwareId);
            Assert.Equal(order.Quantity, result.Quantity);
            Assert.Equal(software.Name, result.Name);
            Assert.Equal(order.ValidTo, result.ValidTo);
        }

        private static Order GetSampleOrder() => new Order()
        {
            AccountId = Guid.NewGuid(),
            Quantity = 1,
            SoftwareId = Guid.NewGuid(),
            ValidTo = DateTime.UtcNow.AddMonths(1)
        };
    }
}

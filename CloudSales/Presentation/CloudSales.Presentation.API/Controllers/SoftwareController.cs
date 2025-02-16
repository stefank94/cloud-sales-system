using CloudSales.Application.Services;
using CloudSales.Domain.Models;
using CloudSales.Presentation.API.DtoModels;
using CloudSales.Presentation.API.Util;
using Microsoft.AspNetCore.Mvc;

namespace CloudSales.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/v1/customers/{customerId}/accounts/{accountId}/purchased-software")]
    public class SoftwareController : ControllerBase
    {
        private readonly ISoftwareService _softwareService;

        public SoftwareController(ISoftwareService softwareService)
        {
            _softwareService = softwareService;
        }

        private const int MinPage = 1;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchasedSoftwareDto>>> GetPurchasedSoftwareAsync(Guid accountId, [FromQuery] int? page)
        {
            var softwares = await _softwareService.GetPurchasedSoftwareForAccountAsync(
                accountId,
                Sanitize.Integer(page, MinPage, null, nameof(page))
            );

            return new OkObjectResult(softwares.Select(x => new PurchasedSoftwareDto()
            {
                Id = x.Id,
                Name = x.Name,
                Quantity = x.Quantity,
                SoftwareId = x.SoftwareId,
                ValidTo = x.ValidTo,
                State = x.State
            }));
        }

        [HttpPost]
        public async Task<ActionResult<PurchasedSoftwareDto>> PurchaseSoftwareAsync(Guid customerId, Guid accountId, [FromBody] OrderDto orderInput)
        {
            var order = new Order()
            {
                AccountId = accountId,
                Quantity = Sanitize.Integer(orderInput.Quantity, 1, null, nameof(Order.Quantity)),
                SoftwareId = orderInput.SoftwareId,
                ValidTo = orderInput.ValidTo
            };

            var purchasedSoftware = await _softwareService.OrderSoftwareAsync(order);

            return new CreatedResult(RelativeSoftwareLocation(customerId, accountId, purchasedSoftware.Id), purchasedSoftware);
        }

        [HttpPut]
        [Route("{purchasedSoftwareId}/quantity")]
        public Task<ActionResult<PurchasedSoftwareDto>> ChangeQuantityAsync(Guid customerId, Guid accountId, Guid purchasedSoftwareId, [FromBody] ChangeQuantityDto changeQuantityDto)
        {
            // TODO
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{purchasedSoftwareId}/extend-license")]
        public Task<ActionResult<PurchasedSoftwareDto>> ExtendLicenseAsync(Guid customerId, Guid accountId, Guid purchasedSoftwareId, [FromBody] ExtendLicenseDto extendLicenseDto)
        {
            // TODO
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{purchasedSoftwareId}/cancel")]
        public Task<ActionResult<PurchasedSoftwareDto>> CancelAsync(Guid customerId, Guid accountId, Guid purchasedSoftwareId)
        {
            // TODO
            throw new NotImplementedException();
        }

        private static string RelativeSoftwareLocation(Guid customerId, Guid accountId, Guid purchasedSoftwareId) => $"/api/v1/customers/{customerId}/accounts/{accountId}/purchased-software/{purchasedSoftwareId}";
    }
}

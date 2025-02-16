using CloudSales.Application.Services;
using CloudSales.Presentation.API.DtoModels;
using CloudSales.Presentation.API.Util;
using Microsoft.AspNetCore.Mvc;

namespace CloudSales.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/v1/customers/{customerId}/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        private const int MinPage = 1;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccountsAsync(Guid customerId, [FromQuery] int? page)
        {
            var accounts = await _accountsService.GetAccountsForCustomerAsync(
                customerId, 
                Sanitize.Integer(page, MinPage, null, nameof(page))
            );
            
            return new OkObjectResult(accounts.Select(x => new AccountDto()
            {
                Id = x.Id,
                Name = x.Name
            }));
        }
    }
}

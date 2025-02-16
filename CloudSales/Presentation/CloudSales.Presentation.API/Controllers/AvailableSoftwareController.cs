using CloudSales.Application.Services;
using CloudSales.Presentation.API.DtoModels;
using CloudSales.Presentation.API.Util;
using Microsoft.AspNetCore.Mvc;

namespace CloudSales.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/v1/available-software")]
    public class AvailableSoftwareController : ControllerBase
    {
        private readonly ISoftwareService _softwareService;

        public AvailableSoftwareController(ISoftwareService softwareService)
        {
            _softwareService = softwareService;
        }

        private const int MinPage = 1;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvailableSoftwareDto>>> GetAvailableSoftwareAsync([FromQuery] int? page)
        {
            var softwares = await _softwareService.GetAvailableSoftwaresAsync(
                Sanitize.Integer(page, MinPage, null, nameof(page))
            );

            return new OkObjectResult(softwares.Select(x => new AvailableSoftwareDto()
            {
                Id = x.Id,
                Name = x.Name
            }));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AvailableSoftwareDto>> GetAvailableSoftwareAsync(Guid id)
        {
            var software = await _softwareService.GetAvailableSoftwareByIdAsync(id);

            if (software is null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(new AvailableSoftwareDto()
            {
                Id = software.Id,
                Name = software.Name,
            });
        }
    }
}

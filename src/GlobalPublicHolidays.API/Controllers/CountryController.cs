using GlobalPublicHolidays.Application.Country.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ApiControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> SupportedList()
        {
            return Ok(await Mediator.Send(new GetSupportedCountiresQuery()));
        }
    }
}

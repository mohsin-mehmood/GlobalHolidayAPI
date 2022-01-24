using GlobalPublicHolidays.Application.Holidays.Queries.CountryYearly;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ApiControllerBase
    {

        [HttpGet]
        [Route("{country}/[action]/{year:int}")]
        public async Task<IActionResult> GetHolidaysForYear(string country, int year, string region)
        {
            return Ok(await Mediator.Send(new GetCountryYearlyHolidaysQuery
            {
                CountryCode = country,
                Year = year,
                Region = region
            }));
        }
    }
}

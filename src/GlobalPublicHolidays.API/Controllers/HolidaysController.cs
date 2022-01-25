using GlobalPublicHolidays.Application.Holidays.Queries.CountryYearly;
using GlobalPublicHolidays.Application.Holidays.Queries.DayStatus;
using Microsoft.AspNetCore.Mvc;
using System;
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



        [HttpGet]
        [Route("{country}/[action]/{day:datetime}")]
        public async Task<IActionResult> GetDayStatus(string country, DateTime day, string region)
        {
            return Ok(await Mediator.Send(new DayStatusQuery
            {
                CountryCode = country,
                Day = day,
                Region = region
            }));
        }

    }
}

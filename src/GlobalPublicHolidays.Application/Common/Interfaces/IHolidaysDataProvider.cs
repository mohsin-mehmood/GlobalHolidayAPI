using GlobalPublicHolidays.Application.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Application.Common.Interfaces
{
    public interface IHolidaysDataProvider
    {
        public Task<IEnumerable<CountryApiResponseModel>> GetSupportedCountriesAsync();

        public Task<IEnumerable<HolidayApiResponseModel>> GetCountryYearlyHolidays(string countryCode, int year, string region);
    }
}

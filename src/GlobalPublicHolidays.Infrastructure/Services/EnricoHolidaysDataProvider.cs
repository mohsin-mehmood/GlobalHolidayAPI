using GlobalPublicHolidays.Application.Common.Interfaces;
using GlobalPublicHolidays.Application.Common.Models;
using GlobalPublicHolidays.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Infrastructure.Services
{


    internal class EnricoHolidaysDataProvider : IHolidaysDataProvider
    {
        private readonly HttpClient _httpClient;

        public EnricoHolidaysDataProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<HolidayApiResponseModel>> GetCountryYearlyHolidays(string countryCode, int year, string region)
        {

            var apiQuery = $"?action=getHolidaysForYear&holidayType=all&country={countryCode}&year={year}";

            if (!string.IsNullOrWhiteSpace(region))
                apiQuery += $"&region={region}";

            var response = await _httpClient.GetAsync(apiQuery);

            return await response.Content.ReadAsAsync<IEnumerable<HolidayApiResponseModel>>();

        }

        public async Task<IEnumerable<CountryApiResponseModel>> GetSupportedCountriesAsync()
        {

            var response = await _httpClient.GetAsync("?action=getSupportedCountries");

            response.EnsureSuccessStatusCode();


            return await response.Content.ReadAsAsync<IEnumerable<CountryApiResponseModel>>();

        }


    }
}

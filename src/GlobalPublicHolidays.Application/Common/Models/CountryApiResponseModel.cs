using System.Collections.Generic;

namespace GlobalPublicHolidays.Application.Common.Models
{
    public class CountryApiResponseModel
    {

        public string CountryCode { get; set; }

        public string FullName { get; set; }

        public IEnumerable<string> HolidayTypes { get; set; }

        public IEnumerable<string> Regions { get; set; }
        public DateApiResponseModel FromDate { get; set; }

        public DateApiResponseModel ToDate { get; set; }
    }
}

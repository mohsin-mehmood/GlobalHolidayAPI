using GlobalPublicHolidays.Application.Holidays.Queries.Common.Dtos;
using System.Collections.Generic;

namespace GlobalPublicHolidays.Application.Holidays.Queries.CountryYearly
{
    public class MonthlyHolidays
    {
        public int Month { get; set; }

        public int Count { get; set; }

        public IEnumerable<HolidayDto> Holidays { get; set; }
    }

    public class CountryYearlyHolidaysDto
    {
        public string CountryCode { get; set; }

        public int Year { get; set; }
        public string Region { get; set; }

        public IEnumerable<MonthlyHolidays> MonthlyHolidays { get; set; }
    }
}

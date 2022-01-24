using System;
using System.Collections.Generic;

namespace GlobalPublicHolidays.Application.Holidays.Queries.CountryYearly
{

    public class HolidayNameDto
    {
        public string Name { get; set; }
        public string Language { get; set; }
    }

    public class HolidayNoteDto
    {
        public string Note { get; set; }
        public string Language { get; set; }
    }


    public class HolidayDto
    {
        public DateTime Date { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime? Observed { get; set; }


        public IEnumerable<HolidayNameDto> Names { get; set; }
        public IEnumerable<HolidayNoteDto> Notes { get; set; }

        public string HolidayType { get; set; }

        public IEnumerable<string> Flags { get; set; }
    }

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

using System;
using System.Collections.Generic;

namespace GlobalPublicHolidays.Application.Country.Queries
{
    public class CountryDto
    {

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }


        public IEnumerable<string> HolidayTypes { get; set; }

        public IEnumerable<string> Regions { get; set; }
    }
}

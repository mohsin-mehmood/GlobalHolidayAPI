using System;
using System.Collections.Generic;

namespace GlobalPublicHolidays.Domain.Entities
{
    public class Country
    {

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public ICollection<HolidayType> HolidayTypes { get; set; }

        public ICollection<CountryRegion> Regions { get; set; }
    }
}

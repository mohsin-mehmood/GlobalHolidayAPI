using System;
using System.Collections.Generic;

namespace GlobalPublicHolidays.Domain.Entities
{
    public class Holiday
    {
        public long Id { get; set; }
        public string CountryCode { get; set; }

        public int Year { get; set; }

        public string Region { get; set; }

        public DateTime Date { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime? ObservedOn { get; set; }

        public ICollection<HolidayName> Names { get; set; }

        public ICollection<HolidayNote> Notes { get; set; }

        public HolidayTypeEnum HolidayTypeId { get; set; }

        public HolidayType HolidayType { get; set; }

        public ICollection<HolidayFlag> Flags { get; set; }

    }
}

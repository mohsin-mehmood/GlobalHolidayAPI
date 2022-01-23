using System.Collections.Generic;

namespace GlobalPublicHolidays.Domain.Entities
{

    public enum HolidayTypeEnum
    {
        public_holiday = 1,
        observance = 2,
        school_holiday = 3,
        other_day = 4,
        extra_working_day = 5,
        postal_holiday = 6
    }

    public class HolidayType
    {
        public HolidayTypeEnum Id { get; set; }
        public string Name { get; set; }

        public ICollection<Country> Countries { get; set; }
    }
}

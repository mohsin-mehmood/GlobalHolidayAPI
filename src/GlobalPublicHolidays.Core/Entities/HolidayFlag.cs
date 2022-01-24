using System.Collections.Generic;

namespace GlobalPublicHolidays.Domain.Entities
{

    public enum HolidayFlagEnum
    {
        SHOP_CLOSING_DAY = 1,
        REGIONAL_HOLIDAY = 2,
        ADDITIONAL_HOLIDAY = 3,
        PART_DAY_HOLIDAY = 4,
        BANK_HOLIDAY = 5
    }
    public class HolidayFlag
    {

        public HolidayFlagEnum Id { get; set; }

        public string Name { get; set; }


        public ICollection<Holiday> Holidays { get; set; }
    }
}

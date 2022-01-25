using GlobalPublicHolidays.Application.Holidays.Queries.Common.Dtos;

namespace GlobalPublicHolidays.Application.Holidays.Queries.DayStatus
{
    public class DayStatusQueryDto
    {

        public string Status { get; set; }

        public HolidayDto HolidayDetails { get; set; }

    }
}

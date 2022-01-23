namespace GlobalPublicHolidays.Application.Common.Models
{
    public class DateApiResponseModel
    {
        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int? DayOfWeek { get; set; }
    }
}

namespace GlobalPublicHolidays.Domain.Entities
{
    public class HolidayNote
    {
        public int Id { get; set; }

        public string Note { get; set; }

        public string Language { get; set; }

        public Holiday Holiday { get; set; }
    }
}

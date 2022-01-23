namespace GlobalPublicHolidays.Domain.Entities
{
    public class HolidayName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public Holiday Holiday { get; set; }
    }
}

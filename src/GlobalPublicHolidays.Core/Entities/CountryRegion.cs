namespace GlobalPublicHolidays.Domain.Entities
{
    public class CountryRegion
    {
        public string CountryCode { get; set; }

        public string RegionName { get; set; }

        public Country Country { get; set; }

    }
}

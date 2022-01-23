using GlobalPublicHolidays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Configurations
{
    public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {

            builder.HasIndex(h => new { h.CountryCode, h.Year, h.Region }).IsUnique();

            builder.Property(h => h.HolidayTypeId).HasConversion<int>();

        }
    }
}

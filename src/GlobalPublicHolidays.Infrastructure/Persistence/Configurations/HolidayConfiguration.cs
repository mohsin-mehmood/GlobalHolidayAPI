using GlobalPublicHolidays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Configurations
{
    public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {

            builder.Property(h => h.Region).IsRequired(false);

            builder.HasIndex(h => h.Year);
            builder.HasIndex(h => new { h.CountryCode, h.Region });

            builder.Property(h => h.HolidayTypeId).HasConversion<int>();

        }
    }
}

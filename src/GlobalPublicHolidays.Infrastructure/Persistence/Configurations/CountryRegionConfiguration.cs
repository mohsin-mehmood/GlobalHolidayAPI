using GlobalPublicHolidays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Configurations
{
    public class CountryRegionConfiguration : IEntityTypeConfiguration<CountryRegion>
    {
        public void Configure(EntityTypeBuilder<CountryRegion> builder)
        {
            builder.Property(c => c.RegionName).HasMaxLength(5);

            builder.HasKey(c => new { c.CountryCode, c.RegionName });

        }
    }
}

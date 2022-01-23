using GlobalPublicHolidays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Configurations
{
    public class HolidayNameConfiguration : IEntityTypeConfiguration<HolidayName>
    {
        public void Configure(EntityTypeBuilder<HolidayName> builder)
        {

            builder.Property(hn => hn.Name).HasMaxLength(250);
            builder.Property(hn => hn.Language).HasMaxLength(10);


        }
    }
}

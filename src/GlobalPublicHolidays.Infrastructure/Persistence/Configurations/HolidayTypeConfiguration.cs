using GlobalPublicHolidays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Configurations
{
    public class HolidayTypeConfiguration : IEntityTypeConfiguration<HolidayType>
    {
        public void Configure(EntityTypeBuilder<HolidayType> builder)
        {

            builder.Property(ht => ht.Id).HasConversion<int>();
            builder.Property(ht => ht.Name).HasMaxLength(75);

            builder.HasData(
                    Enum.GetValues(typeof(HolidayTypeEnum))
                        .Cast<HolidayTypeEnum>()
                        .Select(ht => new HolidayType { Id = ht, Name = ht.ToString() })
                        );
        }
    }
}

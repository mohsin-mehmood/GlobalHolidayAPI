using GlobalPublicHolidays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Configurations
{
    public class HolidayFlagConfiguration : IEntityTypeConfiguration<HolidayFlag>
    {
        public void Configure(EntityTypeBuilder<HolidayFlag> builder)
        {
            builder.Property(hf => hf.Id).HasConversion<int>();
            builder.Property(hf => hf.Name).HasMaxLength(75);

            builder.HasData(
                  Enum.GetValues(typeof(HolidayFlagEnum))
                      .Cast<HolidayFlagEnum>()
                      .Select(hf => new HolidayFlag { Id = hf, Name = hf.ToString() })
                      );
        }
    }
}

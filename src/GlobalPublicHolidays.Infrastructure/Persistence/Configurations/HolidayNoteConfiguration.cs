using GlobalPublicHolidays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GlobalPublicHolidays.Infrastructure.Persistence.Configurations
{
    public class HolidayNoteConfiguration : IEntityTypeConfiguration<HolidayNote>
    {
        public void Configure(EntityTypeBuilder<HolidayNote> builder)
        {

            builder.Property(hn => hn.Note).HasMaxLength(750);
            builder.Property(hn => hn.Language).HasMaxLength(10);

        }
    }
}

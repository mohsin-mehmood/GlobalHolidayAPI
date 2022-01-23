using GlobalPublicHolidays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Domain.Entities.Country> Countries { get; set; }
        DbSet<CountryRegion> Regions { get; set; }
        DbSet<Holiday> Holidays { get; set; }

        DbSet<HolidayType> HolidayTypes { get; set; }

        DbSet<HolidayFlag> HolidayFlags { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbContext Context { get; }
    }
}

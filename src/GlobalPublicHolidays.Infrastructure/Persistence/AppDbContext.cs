using GlobalPublicHolidays.Application.Common.Interfaces;
using GlobalPublicHolidays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GlobalPublicHolidays.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryRegion> Regions { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        public DbSet<HolidayType> HolidayTypes { get; set; }

        public DbSet<HolidayFlag> HolidayFlags { get; set; }

        public DbContext Context => this;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

        }
    }
}

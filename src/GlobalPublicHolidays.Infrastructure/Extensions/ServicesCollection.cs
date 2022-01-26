using GlobalPublicHolidays.Application.Common.Interfaces;
using GlobalPublicHolidays.Infrastructure.Persistence;
using GlobalPublicHolidays.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalPublicHolidays.Infrastructure.Extensions
{
    public static class ServicesCollection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient<IHolidaysDataProvider, EnricoHolidaysDataProvider>(cfg =>
            {
                cfg.BaseAddress = new System.Uri(configuration["AppSettings:EnricoApiBaseAddress"]);
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        o => o.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
                // options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());


            return services;
        }
    }
}

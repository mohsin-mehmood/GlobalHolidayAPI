using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GlobalPublicHolidays.Application.Extensions
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

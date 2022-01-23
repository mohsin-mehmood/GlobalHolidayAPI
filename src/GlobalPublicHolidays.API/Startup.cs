using GlobalPublicHolidays.API.Filters;
using GlobalPublicHolidays.Application.Extensions;
using GlobalPublicHolidays.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GlobalPublicHolidays.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(_configuration);

            //services.AddLogging(loggingBuilder => {
            //    loggingBuilder.AddConsole()
            //        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            //    loggingBuilder.AddDebug();
            //});

            services.AddSwaggerDocument(doc => doc.Title = "Global Holidays API");

            services.AddControllers(cfg => cfg.Filters.Add<ApiExceptionFilterAttribute>());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }

    }
}

using AutoMapper;
using GlobalPublicHolidays.Application.Holidays.Queries.Common.Dtos;
using GlobalPublicHolidays.Application.Holidays.Queries.CountryYearly;
using GlobalPublicHolidays.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GlobalPublicHolidays.Application.Common.Mappings.Custom
{
    public class CountryYearlyHolidaysConverter : ITypeConverter<IEnumerable<Holiday>, CountryYearlyHolidaysDto>
    {
        public CountryYearlyHolidaysDto Convert(IEnumerable<Holiday> source, CountryYearlyHolidaysDto destination, ResolutionContext context)
        {


            var result = new CountryYearlyHolidaysDto
            {
                CountryCode = (string)context.Items["CountryCode"],
                Region = (string)context.Items["Region"],
                Year = (int)context.Items["Year"]
            };

            if (source.Any())
            {
                var holidaysGrouped = source.GroupBy(g => new { g.CountryCode, g.Region, g.Year });


                foreach (var hg in holidaysGrouped)
                {

                    var monthlyHolidaysGrps = hg.GroupBy(mg => (mg.ObservedOn ?? mg.Date).Month);

                    var monthHolidaysList = new List<MonthlyHolidays>();

                    foreach (var monthHolidayGrp in monthlyHolidaysGrps)
                    {

                        monthHolidaysList.Add(new MonthlyHolidays
                        {
                            Month = monthHolidayGrp.Key,
                            Holidays = context.Mapper.Map<IEnumerable<HolidayDto>>(monthHolidayGrp.Select(mg => mg)),
                            Count = monthHolidayGrp.Count()

                        });
                    }

                    result.MonthlyHolidays = monthHolidaysList.OrderBy(m => m.Month);


                }
            }

            return result;
        }
    }
}

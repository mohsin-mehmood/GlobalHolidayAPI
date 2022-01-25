using AutoMapper;
using GlobalPublicHolidays.Application.Common.Interfaces;
using GlobalPublicHolidays.Application.Holidays.Commands;
using GlobalPublicHolidays.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Application.Holidays.Queries.CountryYearly
{
    public class GetCountryYearlyHolidaysQuery : IRequest<CountryYearlyHolidaysDto>
    {

        public string CountryCode { get; set; }
        public int Year { get; set; }

        public string Region { get; set; }
    }


    public class GetCountryYearlyHolidaysQueryHandler : IRequestHandler<GetCountryYearlyHolidaysQuery, CountryYearlyHolidaysDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public GetCountryYearlyHolidaysQueryHandler(IAppDbContext appDbContext, ISender sender, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<CountryYearlyHolidaysDto> Handle(GetCountryYearlyHolidaysQuery request, CancellationToken cancellationToken)
        {


            IEnumerable<Holiday> countryYearlyHolidays = _appDbContext.Holidays
                                                     .AsNoTracking()
                                                     .Include(c => c.Names)
                                                     .Include(c => c.Notes)
                                                     .Include(c => c.Flags)
                                                     .Include(c => c.HolidayType)
                                                     .Where(h => h.CountryCode == request.CountryCode
                                                                 && h.Year == request.Year
                                                                 && (string.IsNullOrEmpty(request.Region)
                                                                        || h.Region == request.Region));


            if (!countryYearlyHolidays.Any())
            {
                // Load Data
                countryYearlyHolidays = await _sender.Send(new LoadYearlyHolidaysDataCommand
                {
                    CountryCode = request.CountryCode,
                    Region = request.Region,
                    Year = request.Year
                }, cancellationToken);
            }


            return _mapper.Map<CountryYearlyHolidaysDto>(countryYearlyHolidays, opts =>
             {
                 opts.Items["CountryCode"] = request.CountryCode;
                 opts.Items["Year"] = request.Year;
                 opts.Items["Region"] = request.Region;
             });

        }
    }
}

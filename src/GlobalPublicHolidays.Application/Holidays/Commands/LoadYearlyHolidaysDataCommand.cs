using AutoMapper;
using GlobalPublicHolidays.Application.Common.Interfaces;
using GlobalPublicHolidays.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Application.Holidays.Commands
{
    public class LoadYearlyHolidaysDataCommand : IRequest<IEnumerable<Holiday>>
    {
        public string CountryCode { get; set; }
        public int Year { get; set; }

        public string Region { get; set; }
    }


    public class LoadYearlyHolidaysDataCommandHandler : IRequestHandler<LoadYearlyHolidaysDataCommand, IEnumerable<Holiday>>
    {
        private readonly IHolidaysDataProvider _holidaysDataProvider;
        private readonly IMapper _mapper;
        private readonly IAppDbContext _appDbContext;


        public LoadYearlyHolidaysDataCommandHandler(IHolidaysDataProvider holidaysDataProvider,
            IMapper mapper, IAppDbContext appDbContext)
        {
            _holidaysDataProvider = holidaysDataProvider;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Holiday>> Handle(LoadYearlyHolidaysDataCommand request, CancellationToken cancellationToken)
        {
            var countryHolidays = await _holidaysDataProvider.GetCountryYearlyHolidays(request.CountryCode, request.Year, request.Region);

            var holidayTypes = _appDbContext.HolidayTypes.ToList();
            var holidayFlags = _appDbContext.HolidayFlags.ToList();

            // Transform API response into domain models
            var countryHolidayEntities = _mapper.Map<IEnumerable<Holiday>>(countryHolidays,
                opts =>
                {
                    opts.Items["CountryCode"] = request.CountryCode;
                    opts.Items["Year"] = request.Year;
                    opts.Items["Region"] = request.Region;
                    opts.Items["holidayTypes"] = holidayTypes;
                    opts.Items["holidayFlags"] = holidayFlags;
                });


            _appDbContext.Holidays.AddRange(countryHolidayEntities);

            var result = await _appDbContext.SaveChangesAsync(cancellationToken);


            return result > 0 ? countryHolidayEntities : Enumerable.Empty<Holiday>();
        }
    }
}

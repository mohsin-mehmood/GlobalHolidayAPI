using GlobalPublicHolidays.Application.Common.Interfaces;
using GlobalPublicHolidays.Application.Common.Models;
using GlobalPublicHolidays.Application.Holidays.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Application.Holidays.Queries
{
    public class GetCountryYearlyHolidaysQuery : IRequest<IEnumerable<HolidayApiResponseModel>>
    {

        public string CountryCode { get; set; }
        public int Year { get; set; }

        public string Region { get; set; }
    }


    public class GetCountryYearlyHolidaysQueryHandler : IRequestHandler<GetCountryYearlyHolidaysQuery, IEnumerable<HolidayApiResponseModel>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ISender _sender;


        public GetCountryYearlyHolidaysQueryHandler(IAppDbContext appDbContext, ISender sender)
        {
            _appDbContext = appDbContext;
            _sender = sender;
        }

        public async Task<IEnumerable<HolidayApiResponseModel>> Handle(GetCountryYearlyHolidaysQuery request, CancellationToken cancellationToken)
        {


            var countryYearlyHolidays = _appDbContext.Holidays.AsNoTracking().Where(h => h.CountryCode == request.CountryCode
                                                                 && h.Year == request.Year
                                                             && (string.IsNullOrEmpty(request.Region) || h.Region == request.Region));
            if (!countryYearlyHolidays.Any())
            {
                // Load Data
                return await _sender.Send(new LoadYearlyHolidaysDataCommand
                {
                    CountryCode = request.CountryCode,
                    Region = request.Region,
                    Year = request.Year
                }, cancellationToken);
            }

            return null;
        }
    }
}

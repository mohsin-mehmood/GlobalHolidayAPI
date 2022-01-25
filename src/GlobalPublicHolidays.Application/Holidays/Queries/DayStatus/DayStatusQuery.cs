using AutoMapper;
using GlobalPublicHolidays.Application.Common.Interfaces;
using GlobalPublicHolidays.Application.Holidays.Commands;
using GlobalPublicHolidays.Application.Holidays.Queries.Common.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Application.Holidays.Queries.DayStatus
{
    public class DayStatusQuery : IRequest<DayStatusQueryDto>
    {
        public string CountryCode { get; set; }

        public string Region { get; set; }

        public DateTime Day { get; set; }
    }

    public class DayStatusQueryHandler : IRequestHandler<DayStatusQuery, DayStatusQueryDto>
    {

        private readonly IAppDbContext _appDbContext;
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public DayStatusQueryHandler(IAppDbContext appDbContext, ISender sender, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _sender = sender;
            _mapper = mapper;
        }


        public async Task<DayStatusQueryDto> Handle(DayStatusQuery request, CancellationToken cancellationToken)
        {

            var countryHolidaysLoaded = _appDbContext.Holidays.Any(h => h.CountryCode == request.CountryCode
                                              && (string.IsNullOrEmpty(request.Region)
                                                                         || h.Region == request.Region));


            if (!countryHolidaysLoaded)
            {
                // Load Data
                await _sender.Send(new LoadYearlyHolidaysDataCommand
                {
                    CountryCode = request.CountryCode,
                    Region = request.Region,
                    Year = request.Day.Year
                }, cancellationToken);
            }



            var holiday = _appDbContext.Holidays.AsNoTracking()
                                                     .Include(c => c.Names)
                                                     .Include(c => c.Notes)
                                                     .Include(c => c.Flags)
                                                     .Include(c => c.HolidayType)
                                                     .FirstOrDefault(h => (h.ObservedOn ?? h.Date).Date.Equals(request.Day.Date));

            var dayStatus = new DayStatusQueryDto { Status = "work day" };

            if (holiday != null)
            {
                dayStatus.Status = "holiday";

                dayStatus.HolidayDetails = _mapper.Map<HolidayDto>(holiday);

            }
            else if (holiday == null && request.Day.DayOfWeek == DayOfWeek.Saturday || request.Day.DayOfWeek == DayOfWeek.Sunday)
            {
                dayStatus.Status = "free day";
            }

            return dayStatus;
        }
    }
}

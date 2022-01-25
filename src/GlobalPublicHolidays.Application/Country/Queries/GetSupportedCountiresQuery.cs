using AutoMapper;
using GlobalPublicHolidays.Application.Common.Interfaces;
using GlobalPublicHolidays.Application.Country.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Application.Country.Queries
{
    public class GetSupportedCountiresQuery : IRequest<IEnumerable<CountryDto>>
    {
    }


    public class GetSupportedQueryHandler : IRequestHandler<GetSupportedCountiresQuery, IEnumerable<CountryDto>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public GetSupportedQueryHandler(IAppDbContext appDbContext, ISender sender, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _sender = sender;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDto>> Handle(GetSupportedCountiresQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Domain.Entities.Country> countries = _appDbContext.Countries
                                        .AsNoTracking()
                                        .Include(c => c.HolidayTypes)
                                        .Include(c => c.Regions);

            if (!countries.Any())
            {
                countries = await _sender.Send(new LoadCountriesDataCommand());
            }


            return _mapper.Map<IEnumerable<CountryDto>>(countries);
        }
    }
}

using AutoMapper;
using GlobalPublicHolidays.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace GlobalPublicHolidays.Application.Country.Commands
{
    public class LoadCountriesDataCommand : IRequest<IEnumerable<Domain.Entities.Country>>
    {

    }

    public class LoadCountriesDataCommandHandler : IRequestHandler<LoadCountriesDataCommand, IEnumerable<Domain.Entities.Country>>
    {
        private readonly IHolidaysDataProvider _holidaysDataProvider;
        private readonly IMapper _mapper;
        private readonly IAppDbContext _appDbContext;

        public LoadCountriesDataCommandHandler(IHolidaysDataProvider holidaysDataProvider,
            IMapper mapper, IAppDbContext appDbContext)
        {
            _holidaysDataProvider = holidaysDataProvider;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }


        public async Task<IEnumerable<Domain.Entities.Country>> Handle(LoadCountriesDataCommand request, CancellationToken cancellationToken)
        {

            var supportedCountries = await _holidaysDataProvider.GetSupportedCountriesAsync();


            var holidayTypes = _appDbContext.HolidayTypes.ToList();


            // Transform API response into domain models


            var countryEntities = _mapper.Map<IEnumerable<Domain.Entities.Country>>(supportedCountries,
                opts => opts.Items["holidayTypes"] = holidayTypes);


            _appDbContext.Countries.AddRange(countryEntities);

            var result = await _appDbContext.SaveChangesAsync(cancellationToken);

            return result > 0 ? countryEntities : Enumerable.Empty<Domain.Entities.Country>();
        }
    }
}

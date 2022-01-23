using GlobalPublicHolidays.Application.Common.Interfaces;
using GlobalPublicHolidays.Application.Common.Models;
using GlobalPublicHolidays.Application.Country.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Application.Country.Queries
{
    public class GetSupportedCountiresQuery : IRequest<IEnumerable<CountryApiResponseModel>>
    {
        public string CountryCode { get; set; }
    }


    public class GetSupportedQueryHandler : IRequestHandler<GetSupportedCountiresQuery, IEnumerable<CountryApiResponseModel>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ISender _sender;

        public GetSupportedQueryHandler(IAppDbContext appDbContext, ISender sender)
        {
            _appDbContext = appDbContext;
            _sender = sender;
        }

        public async Task<IEnumerable<CountryApiResponseModel>> Handle(GetSupportedCountiresQuery request, CancellationToken cancellationToken)
        {
            var country = _appDbContext.Countries.AsNoTracking().FirstOrDefault(c => c.Code.Equals(request.CountryCode));


            if (country == null)
            {
                return await _sender.Send(new LoadCountriesDataCommand());
            }


            return null;
        }
    }
}

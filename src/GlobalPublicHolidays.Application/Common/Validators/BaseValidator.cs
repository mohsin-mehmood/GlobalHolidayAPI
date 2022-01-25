using FluentValidation;
using GlobalPublicHolidays.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalPublicHolidays.Application.Common.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        private readonly IAppDbContext _appDbContext;

        public BaseValidator(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }

        protected async Task<bool> ValidateRegionAsync(string countryCode, string region, CancellationToken cancellationToken)
        {
            var countryRegions = await _appDbContext.Regions.Where(r => r.CountryCode.ToLower().Equals(countryCode.ToLower())).ToListAsync();


            if (countryRegions.Any() && !string.IsNullOrWhiteSpace(region))
                return countryRegions.Any(cr => cr.RegionName.Equals(region, System.StringComparison.OrdinalIgnoreCase));
            else if (countryRegions.Any() && string.IsNullOrWhiteSpace(region))
                return false;

            return string.IsNullOrWhiteSpace(region);
        }
    }
}

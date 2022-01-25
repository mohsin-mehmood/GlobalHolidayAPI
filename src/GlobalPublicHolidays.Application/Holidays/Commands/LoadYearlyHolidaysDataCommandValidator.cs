using FluentValidation;
using GlobalPublicHolidays.Application.Common.Interfaces;
using GlobalPublicHolidays.Application.Common.Validators;

namespace GlobalPublicHolidays.Application.Holidays.Commands
{
    public class LoadYearlyHolidaysDataCommandValidator : BaseValidator<LoadYearlyHolidaysDataCommand>
    {

        public LoadYearlyHolidaysDataCommandValidator(IAppDbContext appDbContext) : base(appDbContext)
        {

            RuleFor(r => r.CountryCode).NotEmpty().MaximumLength(3).WithMessage("Country Code must be ISO 3166-1 alpha-3 or alpha-2 code");

            RuleFor(r => r.Year).GreaterThan(0).WithMessage("Year must be greater than 0");

            RuleFor(r => r.Region).MustAsync(async (req, region, cancellation) =>
            {
                return await ValidateRegionAsync(req.CountryCode, region, cancellation);
            }).WithMessage("Invalid Region");

        }

    }
}

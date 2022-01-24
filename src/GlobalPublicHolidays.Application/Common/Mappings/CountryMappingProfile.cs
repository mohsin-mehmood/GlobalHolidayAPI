using GlobalPublicHolidays.Application.Common.Models;
using GlobalPublicHolidays.Application.Country.Queries;
using GlobalPublicHolidays.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GlobalPublicHolidays.Application.Common.Mappings
{
    public class CountryMappingProfile : BaseMapperProfile
    {

        private IEnumerable<HolidayType> MapToHolidayTypes(IEnumerable<string> apiHolidayTypes, IEnumerable<HolidayType> holidayTypes)
        {

            return apiHolidayTypes.Select(sh =>
             {
                 return MapToHolidayType(sh, holidayTypes);
             });
        }


        public CountryMappingProfile()
        {

            CreateMap<CountryApiResponseModel, Domain.Entities.Country>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.CountryCode))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.ToDate, opt => opt.MapFrom(src => ConvertToDateTime(src.ToDate)))
                .ForMember(dest => dest.FromDate, opt => opt.MapFrom(src => ConvertToDateTime(src.FromDate)))
                .ForMember(dest => dest.HolidayTypes, opt => opt.MapFrom((src, dest, destMember, context) => MapToHolidayTypes(src.HolidayTypes, (IEnumerable<HolidayType>)context.Items["holidayTypes"])))
                .ForMember(dest => dest.Regions, opt => opt.MapFrom(src =>
                                       src.Regions.Select(r => new CountryRegion
                                       { CountryCode = src.CountryCode, RegionName = r })));


            CreateMap<Domain.Entities.Country, CountryDto>()
              .ForMember(c => c.HolidayTypes, o => o.MapFrom(src => src.HolidayTypes.Select(ht => ht.Name)))
              .ForMember(c => c.Regions, o => o.MapFrom(src => src.Regions.Select(r => r.RegionName)));
        }
    }
}

using AutoMapper;
using GlobalPublicHolidays.Application.Common.Models;
using GlobalPublicHolidays.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalPublicHolidays.Application.Common.Mappings
{
    public class CountryMappingProfile : Profile
    {

        private DateTime? ConvertToDateTime(DateApiResponseModel source)
        {


            if (source == null)
                return null;

            try
            {
                return new DateTime(source.Year, source.Month, source.Day);
            }
            catch
            {
                return null;
            }
        }

        private IEnumerable<HolidayType> MapToHolidayTypes(IEnumerable<string> apiHolidayTypes, IEnumerable<HolidayType> holidayTypes)
        {

            return apiHolidayTypes.Select(sh =>
             {
                 return holidayTypes.First(ht => ht.Name.Equals(sh));

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
        }
    }
}

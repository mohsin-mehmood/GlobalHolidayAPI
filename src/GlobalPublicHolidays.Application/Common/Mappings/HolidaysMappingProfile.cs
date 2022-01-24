using GlobalPublicHolidays.Application.Common.Models;
using GlobalPublicHolidays.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GlobalPublicHolidays.Application.Common.Mappings
{
    public class HolidaysMappingProfile : BaseMapperProfile
    {
        private IEnumerable<HolidayFlag> MapToHolidayFlags(IEnumerable<string> apiHolidayFlags, IEnumerable<HolidayFlag> holidayFlagEntities)
        {
            if (apiHolidayFlags == null || !apiHolidayFlags.Any())
                return Enumerable.Empty<HolidayFlag>();

            return apiHolidayFlags.Select(flag =>
            {
                return MapToHolidayFlag(flag, holidayFlagEntities);
            });
        }

        public HolidaysMappingProfile()
        {
            CreateMap<LangTextApiResponseModel, HolidayName>()
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Text))
                .ForMember(dest => dest.Language, o => o.MapFrom(src => src.Lang));

            CreateMap<LangTextApiResponseModel, HolidayNote>()
                .ForMember(dest => dest.Note, o => o.MapFrom(src => src.Text))
                .ForMember(dest => dest.Language, o => o.MapFrom(src => src.Lang));

            CreateMap<HolidayApiResponseModel, Holiday>()
                .ForMember(dest => dest.CountryCode, o => o.MapFrom((src, dest, destMember, ctx) => ctx.Items["CountryCode"]))
                .ForMember(dest => dest.Year, o => o.MapFrom((src, dest, destMember, ctx) => ctx.Items["Year"]))
                .ForMember(dest => dest.Region, o => o.MapFrom((src, dest, destMember, ctx) => ctx.Items["Region"]))
                .ForMember(dest => dest.Date, o => o.MapFrom(src => ConvertToDateTime(src.Date)))
                .ForMember(dest => dest.DateTo, o => o.MapFrom(src => ConvertToDateTime(src.DateTo)))
                .ForMember(dest => dest.ObservedOn, o => o.MapFrom(src => ConvertToDateTime(src.ObservedOn)))
                .ForMember(dest => dest.Names, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.Notes, o => o.MapFrom(src => src.Note))
                .ForMember(dest => dest.HolidayType, o => o.MapFrom((src, dest, destMem, ctx) =>
                            MapToHolidayType(src.HolidayType, (IEnumerable<HolidayType>)ctx.Items["holidayTypes"])))
                .ForMember(dest => dest.Flags, o => o.MapFrom((src, dest, destMem, ctx) =>
                            MapToHolidayFlags(src.Flags, (IEnumerable<HolidayFlag>)ctx.Items["holidayFlags"])));

        }
    }
}

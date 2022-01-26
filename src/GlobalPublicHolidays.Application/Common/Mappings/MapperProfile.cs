using AutoMapper;
using GlobalPublicHolidays.Application.Common.Mappings.Custom;
using GlobalPublicHolidays.Application.Common.Models;
using GlobalPublicHolidays.Application.Country.Queries;
using GlobalPublicHolidays.Application.Holidays.Queries.Common.Dtos;
using GlobalPublicHolidays.Application.Holidays.Queries.CountryYearly;
using GlobalPublicHolidays.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalPublicHolidays.Application.Common.Mappings
{
    public class MapperProfile : Profile
    {
        private HolidayType MapToHolidayType(string holidayName, IEnumerable<HolidayType> holidayEntities)
        {
            if (string.IsNullOrWhiteSpace(holidayName))
                return null;

            return holidayEntities.First(ht => ht.Name.Equals(holidayName));
        }

        private HolidayFlag MapToHolidayFlag(string flagName, IEnumerable<HolidayFlag> holidayFlagEntities)
        {
            if (string.IsNullOrWhiteSpace(flagName))
                return null;

            return holidayFlagEntities.First(f => f.Name.Equals(flagName));
        }


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
            if (apiHolidayTypes == null)
                return Enumerable.Empty<HolidayType>();

            return apiHolidayTypes.Select(sh =>
            {
                return MapToHolidayType(sh, holidayTypes);
            });
        }

        private IEnumerable<HolidayFlag> MapToHolidayFlags(IEnumerable<string> apiHolidayFlags, IEnumerable<HolidayFlag> holidayFlagEntities)
        {
            if (apiHolidayFlags == null || !apiHolidayFlags.Any())
                return Enumerable.Empty<HolidayFlag>();

            return apiHolidayFlags.Select(flag =>
            {
                return MapToHolidayFlag(flag, holidayFlagEntities);
            });
        }


        private void CountryMappings()
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

        private void HolidaysMappings()
        {
            CreateMap<LangTextApiResponseModel, HolidayName>()
                .ForMember(dest => dest.Holiday, o => o.Ignore())
                .ForMember(dest => dest.Id, o => o.Ignore())
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Text))
                .ForMember(dest => dest.Language, o => o.MapFrom(src => src.Lang));

            CreateMap<LangTextApiResponseModel, HolidayNote>()
                .ForMember(dest => dest.Holiday, o => o.Ignore())
                .ForMember(dest => dest.Id, o => o.Ignore())
                .ForMember(dest => dest.Note, o => o.MapFrom(src => src.Text))
                .ForMember(dest => dest.Language, o => o.MapFrom(src => src.Lang));

            CreateMap<HolidayApiResponseModel, Holiday>()
                .ForMember(dest => dest.HolidayTypeId, o => o.Ignore())
                .ForMember(dest => dest.Id, o => o.Ignore())
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


            CreateMap<HolidayName, HolidayNameDto>();
            CreateMap<HolidayNote, HolidayNoteDto>();

            CreateMap<Holiday, HolidayDto>()
                .ForMember(h => h.Observed, o => o.Ignore())
                .ForMember(h => h.HolidayType, o => o.MapFrom(src => src.HolidayType.Name))
                .ForMember(h => h.Flags, o => o.MapFrom(src => src.Flags.Select(f => f.Name)));

            CreateMap<IEnumerable<Holiday>, CountryYearlyHolidaysDto>().ConvertUsing<CountryYearlyHolidaysConverter>();
        }

        public MapperProfile()
        {

            CountryMappings();
            HolidaysMappings();
        }




    }
}

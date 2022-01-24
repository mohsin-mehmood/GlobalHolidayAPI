using AutoMapper;
using GlobalPublicHolidays.Application.Common.Models;
using GlobalPublicHolidays.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalPublicHolidays.Application.Common.Mappings
{
    public class BaseMapperProfile : Profile
    {
        protected HolidayType MapToHolidayType(string holidayName, IEnumerable<HolidayType> holidayEntities)
        {
            return holidayEntities.First(ht => ht.Name.Equals(holidayName));
        }

        protected HolidayFlag MapToHolidayFlag(string flagName, IEnumerable<HolidayFlag> holidayFlagEntities)
        {
            return holidayFlagEntities.First(f => f.Name.Equals(flagName));
        }


        protected DateTime? ConvertToDateTime(DateApiResponseModel source)
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
    }
}

using AutoMapper;
using FluentAssertions;
using GlobalPublicHolidays.Application.Common.Mappings;
using GlobalPublicHolidays.Application.Common.Models;
using GlobalPublicHolidays.Application.Country.Queries;
using GlobalPublicHolidays.Application.Holidays.Queries.Common.Dtos;
using GlobalPublicHolidays.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Xunit;

namespace GlobalPublicHolidays.Application.Tests.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;


        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = _configuration.CreateMapper();

        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }



        [Theory]
        [InlineData(typeof(CountryApiResponseModel), typeof(Domain.Entities.Country))]
        [InlineData(typeof(Domain.Entities.Country), typeof(CountryDto))]
        [InlineData(typeof(HolidayApiResponseModel), typeof(Holiday))]
        [InlineData(typeof(Holiday), typeof(HolidayDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);
            var contextItems = GetMockContextData();
            var mappedObject = _mapper.Map(instance, source, destination, opts =>
             {
                 foreach (var entry in contextItems)
                 {
                     opts.Items[entry.Key] = entry.Value;
                 }
             });


            mappedObject.Should().NotBeNull();
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }


        private IEnumerable<HolidayType> GetHolidayTypes()
        {
            return Enum.GetValues(typeof(HolidayTypeEnum))
                        .Cast<HolidayTypeEnum>()
                        .Select(ht => new HolidayType { Id = ht, Name = ht.ToString() });
        }

        private IEnumerable<HolidayFlag> GetHolidayFlags()
        {
            return Enum.GetValues(typeof(HolidayFlagEnum))
                        .Cast<HolidayFlagEnum>()
                        .Select(hf => new HolidayFlag { Id = hf, Name = hf.ToString() });
        }


        private IDictionary<string, object> GetMockContextData()
        {
            return new Dictionary<string, object>
            {
                { "holidayTypes", GetHolidayTypes() },
                {"holidayFlags", GetHolidayFlags() },
                {"CountryCode","aus"},
                {"Region", "nsw" },
                {"Year", DateTime.Now.Year }
            };
        }
    }
}

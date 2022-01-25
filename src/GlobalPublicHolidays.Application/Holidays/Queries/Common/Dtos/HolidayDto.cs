using System;
using System.Collections.Generic;

namespace GlobalPublicHolidays.Application.Holidays.Queries.Common.Dtos
{

    public class HolidayDto
    {
        public DateTime Date { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime? Observed { get; set; }


        public IEnumerable<HolidayNameDto> Names { get; set; }
        public IEnumerable<HolidayNoteDto> Notes { get; set; }

        public string HolidayType { get; set; }

        public IEnumerable<string> Flags { get; set; }
    }
}

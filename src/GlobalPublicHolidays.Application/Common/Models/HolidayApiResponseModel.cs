using System.Collections.Generic;

namespace GlobalPublicHolidays.Application.Common.Models
{
    public class HolidayApiResponseModel
    {
        public DateApiResponseModel Date { get; set; }

        public DateApiResponseModel DateTo { get; set; }
        public DateApiResponseModel ObservedOn { get; set; }

        public IEnumerable<LangTextApiResponseModel> Name { get; set; }


        public IEnumerable<LangTextApiResponseModel> Note { get; set; }

        public string HolidayType { get; set; }

        public IEnumerable<string> Flags { get; set; }

    }
}

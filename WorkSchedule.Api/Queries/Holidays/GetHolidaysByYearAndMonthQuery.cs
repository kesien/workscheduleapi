using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Holidays
{
    public class GetHolidaysByYearAndMonthQuery : IRequest<List<HolidayDto>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}

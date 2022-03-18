using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Holidays
{
    public class GetAllHolidaysQuery : IRequest<List<HolidayDto>>
    {
    }
}

using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Holidays
{
    public class GetYearsForAllHolidaysQuery : IRequest<HolidayYearsDto>
    {
    }
}

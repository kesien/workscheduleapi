using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Schedules
{
    public class GetScheduleByDateQuery : IRequest<ScheduleDto>
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}

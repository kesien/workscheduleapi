using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Schedules
{
    public class UpdateScheduleCommand : IRequest<ScheduleDto>
    {
        public List<DayDto> Days { get; set; }
        public string UserId { get; set; }
        public string Id { get; set; }
    }
}

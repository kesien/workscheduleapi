using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Schedules
{
    public class AddNewScheduleCommand : IRequest<ScheduleDto>
    {
        public string UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}

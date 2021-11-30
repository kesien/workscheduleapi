using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
{
    public interface IScheduleService
    {
        public Task<MonthlySchedule?> CreateSchedule(ScheduleDto scheduleDto);
        public Task<MonthlySchedule> GetSchedule(int year, int month);
        public Task<bool> UpdateSchedule(Guid id, List<DayDto> dayDtos);
        public Task<bool> DeleteSchedule(Guid id);
    }
}

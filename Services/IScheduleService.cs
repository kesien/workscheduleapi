using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
{
    public interface IScheduleService
    {
        public Task<MonthlySchedule?> CreateSchedule(ScheduleDto scheduleDto);
        public Task<MonthlySchedule> GetSchedule(int year, int month);
        public Task UpdateSchedule(Guid id, UpdateScheduleDto updateScheduleDto);
        public Task DeleteSchedule(Guid id);
    }
}

using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
{
    public interface IScheduleService
    {
        public Task<MonthlySchedule?> CreateSchedule(int year, int month);
        public Task<MonthlySchedule> GetSchedule(int year, int month);
        public Task<bool> CheckSchedule(int year, int month);
        public Task<MonthlySchedule> UpdateSchedule(Guid id, List<DayDto> dayDtos);
        public Task<bool> DeleteSchedule(Guid id);
    }
}

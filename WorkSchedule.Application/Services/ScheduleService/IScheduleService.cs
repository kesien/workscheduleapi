using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Services.ScheduleService
{
    public interface IScheduleService
    {
        public Task<MonthlySchedule?> CreateSchedule(int year, int month);
        public Task<MonthlySchedule> GetSchedule(int year, int month);
        public Task<bool> CheckSchedule(int year, int month);
        public Task<MonthlySchedule> UpdateSchedule(object id, List<DayDto> dayDtos);
        public Task<MonthlySchedule> DeleteSchedule(object id);
    }
}

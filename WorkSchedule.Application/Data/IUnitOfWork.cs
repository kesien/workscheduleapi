using WorkSchedule.Application.Data.Repositories;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Data
{
    public interface IUnitOfWork
    {
        IRepository<Request> RequestRepository { get; }
        IRepository<Holiday> HolidayRepository { get; }
        IRepository<Summary> SummaryRepository { get; }
        IRepository<WordFile> WordFileRepository { get; }
        ScheduleRepository ScheduleRepository { get; }
        IRepository<Day> DayRepository { get; }
        UsersRepository UserRepository { get; }
        void Dispose();
        void Save();
        void ClearTracking();
    }
}
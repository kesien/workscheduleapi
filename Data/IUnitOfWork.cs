using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduleMaker.Data.Repositories;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Data
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
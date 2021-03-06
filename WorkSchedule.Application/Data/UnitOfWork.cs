using WorkSchedule.Application.Data.Repositories;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Data
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Request> _requestRepository;
        private IRepository<Holiday> _holidayRepository;
        private IRepository<Summary> _summaryRepository;
        private IRepository<WordFile> _wordFileRepository;
        private ScheduleRepository _scheduleRepository;
        private IRepository<Day> _dayRepository;
        private UsersRepository _userRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<WordFile> WordFileRepository
        {
            get
            {
                if (_wordFileRepository == null)
                {
                    _wordFileRepository = new Repository<WordFile>(_context);
                }
                return _wordFileRepository;
            }
        }

        public UsersRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UsersRepository(_context);
                }
                return _userRepository;
            }
        }

        public IRepository<Request> RequestRepository
        {
            get
            {

                if (_requestRepository == null)
                {
                    _requestRepository = new Repository<Request>(_context);
                }
                return _requestRepository;
            }
        }

        public IRepository<Holiday> HolidayRepository
        {
            get
            {

                if (_holidayRepository == null)
                {
                    _holidayRepository = new Repository<Holiday>(_context);
                }
                return _holidayRepository;
            }
        }

        public IRepository<Summary> SummaryRepository
        {
            get
            {

                if (_summaryRepository == null)
                {
                    _summaryRepository = new Repository<Summary>(_context);
                }
                return _summaryRepository;
            }
        }

        public ScheduleRepository ScheduleRepository
        {
            get
            {

                if (_scheduleRepository == null)
                {
                    _scheduleRepository = new ScheduleRepository(_context);
                }
                return _scheduleRepository;
            }
        }

        public IRepository<Day> DayRepository
        {
            get
            {

                if (_dayRepository == null)
                {
                    _dayRepository = new Repository<Day>(_context);
                }
                return _dayRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void ClearTracking()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
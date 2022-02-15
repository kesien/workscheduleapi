using System.Linq.Expressions;
using AutoMapper;
using WorkSchedule.Application.Data;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Services.HolidayService
{
    public class HolidayService : IHolidayService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HolidayService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Holiday>? Add(int day, int month, int year, bool isFix)
        {
            var holidays = await Find(h => h.Day == day && h.Month == month);
            if (holidays.Any() && isFix)
            {
                return null;
            }
            if (!isFix && holidays.Where(h => h.Year == year).Any())
            {
                return null;
            }
            var holiday = new Holiday() { Day = day, Month = month, Year = year, IsFix = isFix };
            _unitOfWork.HolidayRepository.Add(holiday);
            _unitOfWork.Save();
            return holiday;
        }

        public async Task<bool> Delete(Guid id)
        {
            var holiday = _unitOfWork.HolidayRepository.GetByID(id);
            if (holiday is null)
            {
                return false;
            }
            _unitOfWork.HolidayRepository.Delete(holiday);
            _unitOfWork.Save();
            return true;
        }

        public async Task<IEnumerable<Holiday>> GetAll()
        {
            var holidays = _unitOfWork.HolidayRepository.Get();
            return holidays;
        }

        public async Task<Holiday> GetByDate(DateTime date)
        {
            var holiday = await _unitOfWork.HolidayRepository.FindAsync(holiday => holiday.Year == date.Year && holiday.Month == date.Month && holiday.Day == date.Day);
            return holiday;
        }

        public async Task<IEnumerable<Holiday>> Find(Expression<Func<Holiday, bool>> predicate)
        {
            var holidays = _unitOfWork.HolidayRepository.Get(predicate, q => q.OrderBy(q => q.Month).ThenBy(q => q.Day));
            return holidays;
        }
    }
}
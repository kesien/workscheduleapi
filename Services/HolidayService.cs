using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using WorkScheduleMaker.Data;
using WorkScheduleMaker.Data.Repositories;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
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

        public async Task<Holiday> Add(HolidayDto holidayDto)
        {
            var holiday = _mapper.Map<Holiday>(holidayDto);
            var holidays = await Find(h => h.Day == holiday.Day && h.Month == holiday.Month);
            if (holidays.Any()) 
            {
                return null;
            }
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using WorkScheduleMaker.Data.Repositories;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IRepository<Holiday> _repository;
        private readonly IMapper _mapper;

        public HolidayService(IRepository<Holiday> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Holiday> Add(HolidayDto holidayDto)
        {
            var holiday = _mapper.Map<Holiday>(holidayDto);

            _repository.Add(holiday);
            await _repository.SaveAsync();
            return holiday;
        }

        public async Task<bool> Delete(Guid id)
        {
            var holiday = await _repository.GetById(id);
            if (holiday is null)
            {
                return false;
            }
            _repository.Delete(holiday);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Holiday>> GetAll()
        {
            var holidays = await _repository.GetAllAsync();
            return holidays;
        }

        public async Task<Holiday> GetByDate(DateTime date)
        {
            var holiday = await _repository.FindAsync(holiday => holiday.Year == date.Year && holiday.Month == date.Month && holiday.Day == date.Day);
            return holiday;
        }

        public async Task<IEnumerable<Holiday>> Find(Expression<Func<Holiday, bool>> predicate)
        {
            var holidays = await _repository.FindAllAsync(predicate);
            return holidays;
        }
    }
}
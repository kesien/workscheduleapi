using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
{
    public interface IHolidayService
    {
        Task<Holiday> Add(HolidayDto holidayDto);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Holiday>> GetAll();
        Task<Holiday> GetByDate(DateTime date);
        Task<IEnumerable<Holiday>> Find(Expression<Func<Holiday, bool>> predicate);
    }
}
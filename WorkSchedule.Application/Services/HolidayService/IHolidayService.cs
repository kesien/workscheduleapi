using System.Linq.Expressions;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Services.HolidayService
{
    public interface IHolidayService
    {
        Task<Holiday>? Add(DateTime date, bool isFix);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Holiday>> GetAll();
        Task<Holiday> GetByDate(DateTime date);
        Task<IEnumerable<Holiday>> Find(Expression<Func<Holiday, bool>> predicate);
    }
}
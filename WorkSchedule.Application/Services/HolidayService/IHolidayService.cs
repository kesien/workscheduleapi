using System.Linq.Expressions;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.Services.HolidayService
{
    public interface IHolidayService
    {
        Task<Holiday>? Add(int year, int month, int day, bool isFix);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Holiday>> GetAll();
        Task<Holiday> GetByDate(DateTime date);
        Task<IEnumerable<Holiday>> Find(Expression<Func<Holiday, bool>> predicate);
    }
}
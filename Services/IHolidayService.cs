using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Services
{
    public interface IHolidayService
    {
        Task<Holiday> Add(HolidayDto holidayDto);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<Holiday>> GetAll();
    }
}
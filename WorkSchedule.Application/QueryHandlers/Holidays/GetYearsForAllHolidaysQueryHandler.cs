using MediatR;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Api.Queries.Holidays;
using WorkSchedule.Application.Data;

namespace WorkSchedule.Application.Querieshandlers.Holidays
{
    public class GetYearsForAllHolidaysQueryHandler : IRequestHandler<GetYearsForAllHolidaysQuery, HolidayYearsDto>
    {
        private readonly IUnitOfWork _uow;

        public GetYearsForAllHolidaysQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<HolidayYearsDto> Handle(GetYearsForAllHolidaysQuery request, CancellationToken cancellationToken)
        {
            var holidays = await _uow.HolidayRepository.Get();
            var years = holidays.Select(holiday => holiday.Year).ToList();
            var result = new HolidayYearsDto();
            result.Years = new();
            foreach (var year in years)
            {
                if (!result.Years.Contains(year))
                {
                    result.Years.Add(year);
                }
            }
            return result;
        }
    }
}

using AutoMapper;
using MediatR;
using WorkSchedule.Api.Queries.Holidays;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Data;

namespace WorkSchedule.Application.QueryHandlers.Holidays
{
    public class GetHolidaysByYearAndMonthQueryHandler : IRequestHandler<GetHolidaysByYearAndMonthQuery, List<HolidayDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetHolidaysByYearAndMonthQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<HolidayDto>> Handle(GetHolidaysByYearAndMonthQuery request, CancellationToken cancellationToken)
        {
            var holidays = await _uow.HolidayRepository.FindAsync(holiday => holiday.Year == request.Year && holiday.Month == request.Month);
            return _mapper.Map<List<HolidayDto>>(holidays);
        }
    }
}

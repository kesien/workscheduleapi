using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Holidays;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Data;

namespace WorkSchedule.Application.QueryHandlers.Holidays
{
    public class GetAllHolidaysQueryHandler : IRequestHandler<GetAllHolidaysQuery, List<HolidayDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllHolidaysQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<HolidayDto>> Handle(GetAllHolidaysQuery request, CancellationToken cancellationToken)
        {
            var holidays = _uow.HolidayRepository.Get();
            return _mapper.Map<List<HolidayDto>>(holidays);
        }
    }
}

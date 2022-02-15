using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Services.HolidayService;

namespace WorkSchedule.Application.CommandHandlers.Holidays
{
    public class AddNewHolidayCommandHandler : IRequestHandler<AddNewHolidayCommand, HolidayDto>
    {
        private readonly IHolidayService _holidayService;
        private readonly IMapper _mapper;

        public AddNewHolidayCommandHandler(IHolidayService holidayService, IMapper mapper)
        {
            _holidayService = holidayService;
            _mapper = mapper;
        }

        public async Task<HolidayDto> Handle(AddNewHolidayCommand request, CancellationToken cancellationToken)
        {
            if (request.Year == 0 && !request.IsFix)
            {
                throw new ApplicationException("Please provide a year for not fix holidays!");
            }
            var result = await _holidayService.Add(request.Day, request.Month, request.Year, request.IsFix);
            if (result is null)
            {
                throw new ApplicationException("There is already a holiday registered for this date!");
            }
            return _mapper.Map<HolidayDto>(result);
        }
    }
}

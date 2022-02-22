using AutoMapper;
using MediatR;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Exceptions;
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
            var validator = new AddNewHolidayCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            var result = await _holidayService.Add(request.Date, request.IsFix);
            if (result is null)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { "There is already a holiday registered for this date!" } };
            }
            return _mapper.Map<HolidayDto>(result);
        }
    }
}

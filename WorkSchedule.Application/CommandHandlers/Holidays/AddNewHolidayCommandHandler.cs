using MediatR;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Services.HolidayService;

namespace WorkSchedule.Application.CommandHandlers.Holidays
{
    public class AddNewHolidayCommandHandler : IRequestHandler<AddNewHolidayCommand, Unit>
    {
        private readonly IHolidayService _holidayService;

        public AddNewHolidayCommandHandler(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        public async Task<Unit> Handle(AddNewHolidayCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddNewHolidayCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            var result = await _holidayService.Add(request.Year, request.Month, request.Day, request.IsFix);
            if (result is null)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { "There is already a holiday registered for this date!" } };
            }
            return Unit.Value;
        }
    }
}

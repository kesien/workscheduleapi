using MediatR;
using Serilog;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Services.HolidayService;

namespace WorkSchedule.Application.CommandHandlers.Holidays
{
    public class AddNewHolidayCommandHandler : IRequestHandler<AddNewHolidayCommand, Unit>
    {
        private readonly IHolidayService _holidayService;
        private readonly ILogger _logger;

        public AddNewHolidayCommandHandler(IHolidayService holidayService, ILogger logger)
        {
            _holidayService = holidayService;
            _logger = logger;
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
            _logger.Information($"A new holiday with ID: {result.Id} for {request.Year}-{request.Month}-{request.Day} with type: {request.IsFix} has been created");
            return Unit.Value;
        }
    }
}

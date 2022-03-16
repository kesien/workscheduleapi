using MediatR;
using Serilog;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Exceptions;

namespace WorkSchedule.Application.CommandHandlers.Holidays
{
    public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger _logger;
        public DeleteHolidayCommandHandler(IUnitOfWork uow, ILogger logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteHolidayCommanddValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validatorResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            var holiday = await _uow.HolidayRepository.GetByID(request.Id);
            if (holiday is not null)
            {
                _uow.HolidayRepository.Delete(holiday);
                _uow.Save();
                _logger.Information($"Holiday with ID: {holiday.Id} has been deleted");
            }
            return Unit.Value;
        }
    }
}

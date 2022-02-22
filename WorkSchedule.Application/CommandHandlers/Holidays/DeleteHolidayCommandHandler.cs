using MediatR;
using WorkSchedule.Api.Commands.Holidays;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Exceptions;

namespace WorkSchedule.Application.CommandHandlers.Holidays
{
    public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteHolidayCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
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
            _uow.HolidayRepository.Delete(holiday);
            _uow.Save();
            return Unit.Value;
        }
    }
}

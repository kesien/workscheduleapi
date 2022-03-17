using FluentValidation;
using MediatR;

namespace WorkSchedule.Api.Commands.Holidays
{
    public class DeleteHolidayCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteHolidayCommanddValidator : AbstractValidator<DeleteHolidayCommand>
    {
        public DeleteHolidayCommanddValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}

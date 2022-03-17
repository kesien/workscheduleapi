using FluentValidation;
using MediatR;

namespace WorkSchedule.Api.Commands.Schedules
{
    public class DeleteScheduleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteScheduleCommandValidator : AbstractValidator<DeleteScheduleCommand>
    {
        public DeleteScheduleCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}

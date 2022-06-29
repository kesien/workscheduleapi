using FluentValidation;
using MediatR;

namespace WorkSchedule.Api.Commands.Schedules
{
    public class AddNewScheduleCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }

    public class AddNewScheduleCommandValidator : AbstractValidator<AddNewScheduleCommand>
    {
        public AddNewScheduleCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty()
                .NotNull();
            RuleFor(c => c.Year)
                .InclusiveBetween(1000, 9999)
                .NotNull();
            RuleFor(c => c.Month)
                .InclusiveBetween(1, 12)
                .NotNull();
        }
    }
}

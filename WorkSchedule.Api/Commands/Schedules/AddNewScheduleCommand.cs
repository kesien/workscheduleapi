using FluentValidation;
using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Schedules
{
    public class AddNewScheduleCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }

    public class AddNewScheduleCommandValidator : AbstractValidator<AddNewScheduleCommand>
    {
        public AddNewScheduleCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .NotNull().WithMessage("{PropertyName} is required!");
            RuleFor(c => c.Year)
                .InclusiveBetween(1000, 9999).WithMessage("{PropertyName} should be between 1000 and 9999")
                .NotNull().WithMessage("{PropertyName} is required!");
            RuleFor(c => c.Month)
                .InclusiveBetween(1, 12).WithMessage("{PropertyName} should be between 1 and 12")
                .NotNull().WithMessage("{PropertyName} is required!");
        }
    }
}

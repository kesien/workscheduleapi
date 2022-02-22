using FluentValidation;
using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Schedules
{
    public class UpdateScheduleCommand : IRequest<ScheduleDto>
    {
        public List<DayDto> Days { get; set; }
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }

    public class UpdateScheduleCommandValidator : AbstractValidator<UpdateScheduleCommand>
    {
        public UpdateScheduleCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotNull().WithMessage("{PropertyName} is required!")
                .NotEmpty().WithMessage("{PropertyName} is required!");
            RuleFor(c => c.Id)
                .NotNull().WithMessage("{PropertyName} is required!")
                .NotEmpty().WithMessage("{PropertyName} is required!");
            RuleFor(c => c.Days)
                .NotNull().WithMessage("{PropertyName} is required!")
                .NotEmpty().WithMessage("{PropertyName} is required!");
            RuleForEach(c => c.Days).ChildRules(days =>
            {
                days.RuleFor(day => day.Date)
                    .NotNull()
                    .NotEmpty();
                days.RuleFor(day => day.UsersScheduledForMorning)
                    .NotNull().WithMessage("{PropertyName} is required!");
                days.RuleFor(day => day.UsersScheduledForForenoon)
                    .NotNull().WithMessage("{PropertyName} is required!");
                days.RuleFor(day => day.UsersOnHoliday)
                    .NotNull().WithMessage("{PropertyName} is required!");
                days.RuleForEach(day => day.UsersScheduledForMorning).ChildRules(users =>
                {
                    users.RuleFor(user => user.Id)
                        .NotNull().WithMessage("{PropertyName} is required!")
                        .NotEmpty().WithMessage("{PropertyName} is required!");
                    users.RuleFor(user => user.Name)
                        .NotNull().WithMessage("{PropertyName} is required!")
                        .NotEmpty().WithMessage("{PropertyName} is required!");
                    users.RuleFor(user => user.UserName)
                        .NotNull().WithMessage("{PropertyName} is required!")
                        .NotEmpty().WithMessage("{PropertyName} is required!");
                    users.RuleFor(user => user.IsRequest)
                        .NotNull().WithMessage("{PropertyName} is required!");
                });
                days.RuleForEach(day => day.UsersScheduledForForenoon).ChildRules(users =>
                {
                    users.RuleFor(user => user.Id)
                        .NotNull().WithMessage("{PropertyName} is required!")
                        .NotEmpty().WithMessage("{PropertyName} is required!");
                    users.RuleFor(user => user.Name)
                        .NotNull().WithMessage("{PropertyName} is required!")
                        .NotEmpty().WithMessage("{PropertyName} is required!");
                    users.RuleFor(user => user.UserName)
                        .NotNull().WithMessage("{PropertyName} is required!")
                        .NotEmpty().WithMessage("{PropertyName} is required!");
                    users.RuleFor(user => user.IsRequest)
                        .NotNull().WithMessage("{PropertyName} is required!");
                });
                days.RuleForEach(day => day.UsersOnHoliday).ChildRules(users =>
                {
                    users.RuleFor(user => user.Id)
                        .NotNull().WithMessage("{PropertyName} is required!")
                        .NotEmpty().WithMessage("{PropertyName} is required!");
                    users.RuleFor(user => user.Name)
                        .NotNull().WithMessage("{PropertyName} is required!")
                        .NotEmpty().WithMessage("{PropertyName} is required!");
                    users.RuleFor(user => user.UserName)
                        .NotNull().WithMessage("{PropertyName} is required!")
                        .NotEmpty().WithMessage("{PropertyName} is required!");
                    users.RuleFor(user => user.IsRequest)
                        .NotNull().WithMessage("{PropertyName} is required!");
                });
            });
        }
    }
}

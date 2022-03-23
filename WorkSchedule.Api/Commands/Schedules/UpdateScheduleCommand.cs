using FluentValidation;
using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Schedules
{
    public class UpdateScheduleCommand : IRequest<Unit>
    {
        public List<DayDto> Days { get; set; }
        public string UserId { get; set; }
        public Guid Id { get; set; }
    }

    public class UpdateScheduleCommandValidator : AbstractValidator<UpdateScheduleCommand>
    {
        public UpdateScheduleCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Days)
                .NotNull()
                .NotEmpty();
            RuleForEach(c => c.Days).ChildRules(days =>
            {
                days.RuleFor(day => day.Date)
                    .NotNull()
                    .NotEmpty();
                days.RuleFor(day => day.UsersScheduledForMorning)
                    .NotNull();
                days.RuleFor(day => day.UsersScheduledForForenoon)
                    .NotNull();
                days.RuleFor(day => day.UsersOnHoliday)
                    .NotNull();
                days.RuleForEach(day => day.UsersScheduledForMorning).ChildRules(users =>
                {
                    users.RuleFor(user => user.Id)
                        .NotNull()
                        .NotEmpty();
                    users.RuleFor(user => user.Name)
                        .NotNull()
                        .NotEmpty();
                    users.RuleFor(user => user.UserName)
                        .NotNull()
                        .NotEmpty();
                    users.RuleFor(user => user.IsRequest)
                        .NotNull();
                });
                days.RuleForEach(day => day.UsersScheduledForForenoon).ChildRules(users =>
                {
                    users.RuleFor(user => user.Id)
                        .NotNull()
                        .NotEmpty();
                    users.RuleFor(user => user.Name)
                        .NotNull()
                        .NotEmpty();
                    users.RuleFor(user => user.UserName)
                        .NotNull()
                        .NotEmpty();
                    users.RuleFor(user => user.IsRequest)
                        .NotNull();
                });
                days.RuleForEach(day => day.UsersOnHoliday).ChildRules(users =>
                {
                    users.RuleFor(user => user.Id)
                        .NotNull()
                        .NotEmpty();
                    users.RuleFor(user => user.Name)
                        .NotNull()
                        .NotEmpty();
                    users.RuleFor(user => user.UserName)
                        .NotNull()
                        .NotEmpty();
                    users.RuleFor(user => user.IsRequest)
                        .NotNull();
                });
            });
        }
    }
}

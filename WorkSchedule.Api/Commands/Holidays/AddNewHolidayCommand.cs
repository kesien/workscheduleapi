using FluentValidation;
using MediatR;

namespace WorkSchedule.Api.Commands.Holidays
{
    public class AddNewHolidayCommand : IRequest<Unit>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public bool IsFix { get; set; }
    }

    public class AddNewHolidayCommandValidator : AbstractValidator<AddNewHolidayCommand>
    {
        public AddNewHolidayCommandValidator()
        {
            this.CascadeMode = CascadeMode.Stop;
            RuleFor(c => c.IsFix).NotNull();
            RuleFor(c => c.Year)
                .NotEmpty()
                .InclusiveBetween(int.MinValue, int.MaxValue);
            RuleFor(c => c.Month)
                .NotEmpty()
                .InclusiveBetween(1, 12);
            RuleFor(c => c.Day)
                .NotEmpty();
            RuleFor(c => c).Must(args =>
            {
                var maxDay = DateTime.DaysInMonth(args.Year, args.Month);
                return maxDay >= args.Day;
            });
        }
    }
}

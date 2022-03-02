using FluentValidation;
using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Holidays
{
    public class AddNewHolidayCommand : IRequest<HolidayDto>
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
            RuleFor(c => c.IsFix).NotNull().WithMessage("{PropertyName} is required!");
            RuleFor(c => c.Year)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty!")
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

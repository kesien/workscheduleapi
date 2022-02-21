using FluentValidation;
using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Holidays
{
    public class AddNewHolidayCommand : IRequest<HolidayDto>
    {
        public DateTime Date { get; set; }
        public bool IsFix { get; set; }
    }

    public class AddNewHolidayCommandValidator : AbstractValidator<AddNewHolidayCommand>
    {
        public AddNewHolidayCommandValidator()
        {
            RuleFor(c => c.IsFix).NotNull().WithMessage("{PropertyName} is required!");
            RuleFor(c => c.Date)
                .Must(BeAValidDate).WithMessage("{PropertyName} is required!")
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty!");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}

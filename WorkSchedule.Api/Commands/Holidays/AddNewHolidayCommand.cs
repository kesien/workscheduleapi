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
            RuleFor(c => c.IsFix).NotEmpty().WithMessage("{PropertyName} shouldn't be empty!");
            RuleFor(c => c.Year).NotEmpty().WithMessage("{PropertyName} shouldn't be empty!");
            RuleFor(c => c.Month).NotEmpty().WithMessage("{PropertyName} shouldn't be empty!");
            RuleFor(c => c.Day).NotEmpty().WithMessage("{PropertyName} shouldn't be empty!");
        }
    }
}

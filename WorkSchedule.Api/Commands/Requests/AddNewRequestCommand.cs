using FluentValidation;
using MediatR;
using WorkSchedule.Api.Constants;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Requests
{
    public class AddNewRequestCommand : IRequest<RequestDto>
    {
        private DateTime _date;
        public Guid UserId { get; set; }
        public DateTime Date 
        { 
            get
            {
                return _date;
            }
            set
            {
                _date = DateTime.Parse(value.ToString());
            }
        }
        public RequestType Type { get; set; }
    }

    public class AddNewRequestCommandValidator : AbstractValidator<AddNewRequestCommand>
    {
        public AddNewRequestCommandValidator()
        {
            RuleFor(c => c.UserId).NotNull().WithMessage("{PropertyName} is required!");
            RuleFor(c => c.Date)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} is required!")
                .Must(BeAValidDate).WithMessage("{PropertyName} must be a valid date!")
                .Must(BeInTheFuture).WithMessage("Invalid date! You can't create a request in the past!")
                .Must(NotBeWeekend).WithMessage("Invalid date! You can't create a request on weekends!")
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty!");
            RuleFor(c => c.Type)
                .NotNull().WithMessage("{PropertyName} is required!");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool BeInTheFuture(DateTime date)
        {
            var dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            var reqDate = new DateTime(date.Year, date.Month, 1, 0, 0, 0);
            int result = DateTime.Compare(reqDate, dateNow);
            return result > -1;
        }

        private bool NotBeWeekend(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}

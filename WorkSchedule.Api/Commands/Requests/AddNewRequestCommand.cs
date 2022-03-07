using FluentValidation;
using MediatR;
using WorkSchedule.Api.Constants;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Requests
{
    public class AddNewRequestCommand : IRequest<Unit>
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
            RuleFor(c => c.UserId).NotNull();
            RuleFor(c => c.Date)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Must(BeAValidDate)
                .Must(BeInTheFuture)
                .Must(NotBeWeekend)
                .NotEmpty();
            RuleFor(c => c.Type)
                .NotNull();
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

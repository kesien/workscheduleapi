using FluentValidation;
using MediatR;
using WorkSchedule.Api.Constants;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Requests
{
    public class AddNewRequestCommand : IRequest<RequestDto>
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
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
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty!");
            RuleFor(c => c.Type)
                .NotNull().WithMessage("{PropertyName} is required!");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}

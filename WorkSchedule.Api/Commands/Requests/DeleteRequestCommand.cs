using FluentValidation;
using MediatR;

namespace WorkSchedule.Api.Commands.Requests
{
    public class DeleteRequestCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteRequestCommandValidator : AbstractValidator<DeleteRequestCommand>
    {
        public DeleteRequestCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}

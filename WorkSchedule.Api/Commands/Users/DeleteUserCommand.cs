using FluentValidation;
using MediatR;

namespace WorkSchedule.Api.Commands.Users
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }

    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}

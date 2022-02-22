using FluentValidation;
using MediatR;
using WorkSchedule.Api.Constants;

namespace WorkSchedule.Api.Commands.Users
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public string RequesterId { get; set; }
        public string Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public UserRole? Role { get; set; }
    }

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.RequesterId)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Id)
                .NotNull()
                .NotEmpty();
            RuleFor(c => c.Username)
                .MinimumLength(5)
                .MaximumLength(80)
                .EmailAddress();
            RuleFor(c => c.Password)
                .MinimumLength(5)
                .MinimumLength(30);
            RuleFor(c => c.Name)
                .MinimumLength(3)
                .MaximumLength(80);
            RuleFor(c => c.Role)
                .IsInEnum();
        }
    }
}

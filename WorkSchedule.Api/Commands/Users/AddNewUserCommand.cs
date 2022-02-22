using FluentValidation;
using MediatR;
using WorkSchedule.Api.Constants;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Commands.Users
{
    public class AddNewUserCommand : IRequest<UserToListDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }

    public class AddNewUserCommandValidator : AbstractValidator<AddNewUserCommand>
    {
        public AddNewUserCommandValidator()
        {
            RuleFor(c => c.Username)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(80)
                .EmailAddress();
            RuleFor(c => c.Password)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(30);
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(80);
            RuleFor(c => c.Role)
                .NotNull()
                .IsInEnum();
        }
    }
}

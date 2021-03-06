using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;
using WorkSchedule.Api.Commands.Users;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.CommandHandlers.Users
{
    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;
        public AddNewUserCommandHandler(UserManager<User> userManager, ILogger logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddNewUserCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validatorResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { "Username already exists!" } };
            }
            var newUser = new User() { UserName = request.Username, Name = request.Name, Role = (Constants.UserRole)request.Role };
            newUser.PasswordHash = _userManager.PasswordHasher.HashPassword(newUser, request.Password);
            await _userManager.CreateAsync(newUser);
            _logger.Information($"New user created with Id: {newUser.Id}");
            if (request.Role == Api.Constants.UserRole.ADMIN)
            {
                await _userManager.AddToRoleAsync(newUser, "ADMINISTRATOR");
            }
            else
            {
                await _userManager.AddToRoleAsync(newUser, "USER");
            }
            return Unit.Value;
        }
    }
}

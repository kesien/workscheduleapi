using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkSchedule.Api.Commands.Users;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.CommandHandlers.Users
{
    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, UserToListDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public AddNewUserCommandHandler(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserToListDto> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
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
            if (request.Role == Api.Constants.UserRole.ADMIN)
            {
                await _userManager.AddToRoleAsync(newUser, "ADMINISTRATOR");
            }
            else
            {
                await _userManager.AddToRoleAsync(newUser, "USER");
            }
            return _mapper.Map<UserToListDto>(newUser);
        }
    }
}

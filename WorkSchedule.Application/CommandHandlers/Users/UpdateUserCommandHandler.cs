using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkSchedule.Api.Commands.Users;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.CommandHandlers.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _uow;

        public UpdateUserCommandHandler(UserManager<User> userManager, IUnitOfWork uow)
        {
            _userManager = userManager;
            _uow = uow;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validatorResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validatorResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            var requester = await _userManager.FindByIdAsync(request.RequesterId);
            var requesterRoles = await _userManager.GetRolesAsync(requester);
            var userToChange = await _userManager.FindByIdAsync(request.Id);
            if (userToChange is null || (request.RequesterId != request.Id && !requesterRoles.Contains("Administrator")))
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { "You don't have enough permission!" } };
            }
            if (!string.IsNullOrEmpty(request.Password) && !string.IsNullOrWhiteSpace(request.Password))
            {
                var passwordHash = _userManager.PasswordHasher.HashPassword(userToChange, request.Password);
                userToChange.PasswordHash = passwordHash;
            }
            userToChange.UserName = request.Username ?? userToChange.UserName;
            if (request.Name is not null && request.Name != userToChange.Name)
            {
                userToChange.Name = request.Name;
                await UpdateSummaries(userToChange.Id, request.Name);
            }
            if (requesterRoles.Contains("Administrator") && request.Role is not null)
            {
                await _userManager.AddToRoleAsync(userToChange, request?.Role?.ToString().ToUpper());
            }
            await _userManager.UpdateAsync(userToChange);
            return Unit.Value;
        }

        private async Task UpdateSummaries(string id, string name)
        {
            var summaries = await _uow.SummaryRepository.Get(summary => summary.UserId == id);
            foreach (var summary in summaries)
            {
                summary.Name = name;
            }
            _uow.Save();
        }
    }
}

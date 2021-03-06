using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Api.Queries.Users;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Persistency.Entities;

namespace WorkSchedule.Application.QueryHandlers.Users
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserToListDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserToListDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RequesterId))
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { "You don't have permission to access this data!" } };
            }
            var requesterUser = await _userManager.FindByIdAsync(request.RequesterId);
            var requesterRoles = await _userManager.GetRolesAsync(requesterUser);
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null || request.Id != request.RequesterId && (!requesterRoles.Contains("Administrator") || !requesterRoles.Contains("Superadmin")))
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { "You don't have permission to access this data!" } };
            }
            return _mapper.Map<UserToListDto>(user);
        }
    }
}

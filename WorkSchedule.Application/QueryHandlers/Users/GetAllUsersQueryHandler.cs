using AutoMapper;
using MediatR;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Api.Queries.Users;
using WorkSchedule.Application.Data;

namespace WorkSchedule.Application.QueryHandlers.Users
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserToListDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<List<UserToListDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _uow.UserRepository.Get(user => user.Role != Constants.UserRole.SUPERADMIN, orderBy: users => users.OrderBy(user => user.Name));
            return _mapper.Map<List<UserToListDto>>(users);
        }
    }
}

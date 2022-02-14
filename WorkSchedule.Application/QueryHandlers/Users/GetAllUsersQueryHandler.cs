using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Users;
using WorkSchedule.Application.Data;
using WorkSchedule.Api.Dtos;

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
            var users = _uow.UserRepository.Get();
            return _mapper.Map<List<UserToListDto>>(users);
        }
    }
}

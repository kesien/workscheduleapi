using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Users
{
    public class GetAllUsersQuery : IRequest<List<UserToListDto>>
    {
    }
}

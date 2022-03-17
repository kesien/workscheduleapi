using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Users
{
    public class GetUserByIdQuery : IRequest<UserToListDto>
    {
        public string RequesterId { get; set; }
        public string Id { get; set; }
    }
}

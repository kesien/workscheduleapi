using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Requests
{
    public class GetAllRequestsForUserQuery : IRequest<List<RequestWithUserDto>>
    {
        public Guid UserId { get; set; }
    }
}

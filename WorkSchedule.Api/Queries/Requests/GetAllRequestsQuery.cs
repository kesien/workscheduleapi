using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Requests
{
    public class GetAllRequestsQuery : IRequest<List<RequestWithUserDto>>
    {
    }
}

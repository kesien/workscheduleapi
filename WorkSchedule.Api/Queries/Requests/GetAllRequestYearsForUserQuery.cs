using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Requests
{
    public class GetAllRequestYearsForUserQuery : IRequest<RequestYearsDto>
    {
        public Guid UserId { get; set; }
    }
}

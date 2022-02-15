using MediatR;

namespace WorkSchedule.Api.Commands.Requests
{
    public class DeleteRequestCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}

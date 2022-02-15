using MediatR;
using WorkSchedule.Api.Commands.Requests;
using WorkSchedule.Application.Services.RequestService;

namespace WorkSchedule.Application.CommandHandlers.Requests
{
    public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, Unit>
    {
        private readonly IRequestService _requestService;

        public DeleteRequestCommandHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<Unit> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
        {
            await _requestService.DeleteRequest(request.Id);
            return Unit.Value;
        }
    }
}

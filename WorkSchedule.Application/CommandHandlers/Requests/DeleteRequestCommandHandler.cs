using MediatR;
using WorkSchedule.Api.Commands.Requests;
using WorkSchedule.Application.Exceptions;
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
            var validator = new DeleteRequestCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            await _requestService.DeleteRequest(request.Id);
            return Unit.Value;
        }
    }
}

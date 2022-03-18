using MediatR;
using Serilog;
using WorkSchedule.Api.Commands.Requests;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Services.RequestService;

namespace WorkSchedule.Application.CommandHandlers.Requests
{
    public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, Unit>
    {
        private readonly IRequestService _requestService;
        private readonly ILogger _logger;
        public DeleteRequestCommandHandler(IRequestService requestService, ILogger logger)
        {
            _requestService = requestService;
            _logger = logger;
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
            _logger.Information($"Request with ID: {request.Id} has been deleted!");
            return Unit.Value;
        }
    }
}

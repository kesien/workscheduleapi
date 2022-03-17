using MediatR;
using Serilog;
using WorkSchedule.Api.Commands.Requests;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Services.RequestService;

namespace WorkSchedule.Application.CommandHandlers.Requests
{
    public class AddNewRequestCommandHandler : IRequestHandler<AddNewRequestCommand, Unit>
    {
        private readonly IRequestService _requestService;
        private readonly ILogger _logger;
        public AddNewRequestCommandHandler(IRequestService requestService, ILogger logger)
        {
            _requestService = requestService;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddNewRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddNewRequestCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList() };
            }
            var newRequest = await _requestService.CreateRequest(request.UserId, request.Date, request.Type);
            if (newRequest is null)
            {
                throw new BusinessException { ErrorCode = 599, ErrorMessages = new List<string> { $"Can't create a request on: {request.Date}" } };
            }
            _logger.Information($"A new request with ID: {newRequest.Id} for user with ID: {request.UserId} has been created");
            return Unit.Value;
        }
    }
}

using MediatR;
using WorkSchedule.Api.Commands.Requests;
using WorkSchedule.Application.Exceptions;
using WorkSchedule.Application.Services.RequestService;

namespace WorkSchedule.Application.CommandHandlers.Requests
{
    public class AddNewRequestCommandHandler : IRequestHandler<AddNewRequestCommand, Unit>
    {
        private readonly IRequestService _requestService;

        public AddNewRequestCommandHandler(IRequestService requestService)
        {
            _requestService = requestService;
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
            return Unit.Value;
        }
    }
}

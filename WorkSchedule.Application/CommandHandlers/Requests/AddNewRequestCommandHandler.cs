using AutoMapper;
using MediatR;
using WorkSchedule.Api.Commands.Requests;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Services.RequestService;

namespace WorkSchedule.Application.CommandHandlers.Requests
{
    public class AddNewRequestCommandHandler : IRequestHandler<AddNewRequestCommand, RequestDto>
    {
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;

        public AddNewRequestCommandHandler(IRequestService requestService, IMapper mapper)
        {
            _requestService = requestService;
            _mapper = mapper;
        }

        public async Task<RequestDto> Handle(AddNewRequestCommand request, CancellationToken cancellationToken)
        {
            var newRequest = await _requestService.CreateRequest(request.UserId, request.Date, request.Type);
            if (newRequest is null)
            {
                throw new ApplicationException($"Can't create a request on: {request.Date}");
            }
            return _mapper.Map<RequestDto>(newRequest);
        }
    }
}

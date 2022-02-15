using AutoMapper;
using MediatR;
using WorkSchedule.Api.Queries.Requests;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Services.RequestService;

namespace WorkSchedule.Application.QueryHandlers.Requests
{
    public class GetAllRequestsForUserQueryHandler : IRequestHandler<GetAllRequestsForUserQuery, List<RequestWithUserDto>>
    {
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;

        public GetAllRequestsForUserQueryHandler(IRequestService requestService, IMapper mapper)
        {
            _requestService = requestService;
            _mapper = mapper;
        }

        public async Task<List<RequestWithUserDto>> Handle(GetAllRequestsForUserQuery request, CancellationToken cancellationToken)
        {
            var requests = await _requestService.GetAllRequestsForUser(request.UserId);
            return _mapper.Map<List<RequestWithUserDto>>(requests);
        }
    }
}

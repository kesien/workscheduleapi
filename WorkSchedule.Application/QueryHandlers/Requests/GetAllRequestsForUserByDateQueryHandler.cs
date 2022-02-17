using AutoMapper;
using MediatR;
using WorkSchedule.Api.Queries.Requests;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Services.RequestService;

namespace WorkSchedule.Application.QueryHandlers.Requests
{
    public class GetAllRequestsForUserByDateQueryHandler : IRequestHandler<GetAllRequestsForUserByDateQuery, List<RequestWithUserDto>>
    {
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;

        public GetAllRequestsForUserByDateQueryHandler(IMapper mapper, IRequestService requestService)
        {
            _mapper = mapper;
            _requestService = requestService;
        }

        public async Task<List<RequestWithUserDto>> Handle(GetAllRequestsForUserByDateQuery request, CancellationToken cancellationToken)
        {
            var requests = await _requestService.GetAllRequestsForUserByDate(request.UserId.ToString(), request.Year, request.Month);
            return _mapper.Map<List<RequestWithUserDto>>(requests);
        }
    }
}

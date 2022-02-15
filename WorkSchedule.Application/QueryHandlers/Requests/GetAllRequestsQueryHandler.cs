using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Queries.Requests;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Application.Services.RequestService;

namespace WorkSchedule.Application.QueryHandlers.Requests
{
    public class GetAllRequestsQueryHandler : IRequestHandler<GetAllRequestsQuery, List<RequestWithUserDto>>
    {
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;

        public GetAllRequestsQueryHandler(IRequestService requestService, IMapper mapper)
        {
            _requestService = requestService;
            _mapper = mapper;
        }

        public async Task<List<RequestWithUserDto>> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
        {
            var requests = await _requestService.GetAllRequests();
            return _mapper.Map<List<RequestWithUserDto>>(requests);
        }
    }
}

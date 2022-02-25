using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Api.Queries.Requests;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Services.RequestService;

namespace WorkSchedule.Application.QueryHandlers.Requests
{
    public class GetAllRequestYearsForUserQueryHandler : IRequestHandler<GetAllRequestYearsForUserQuery, RequestYearsDto>
    {
        private readonly IRequestService _requestService;

        public GetAllRequestYearsForUserQueryHandler(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<RequestYearsDto> Handle(GetAllRequestYearsForUserQuery request, CancellationToken cancellationToken)
        {
            var requests = await _requestService.GetAllRequestsForUser(request.UserId);
            var years = requests.Select(r => r.Date.Year).ToList();
            var response = new RequestYearsDto();
            response.Years = new();
            foreach (var year in years)
            {
                if (!response.Years.Contains(year))
                {
                    response.Years.Add(year);
                }
            }
            return response;
        }
    }
}

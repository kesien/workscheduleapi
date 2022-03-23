using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Dtos;
using WorkSchedule.Api.Queries.Statistics;
using WorkSchedule.Application.Data;

namespace WorkSchedule.Application.QueryHandlers.Statistics
{
    public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, StatisticsDto>
    {
        private readonly IUnitOfWork _uow;

        public GetStatisticsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<StatisticsDto> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            var result = new StatisticsDto();
            result.Requests = new();

            var requests = await _uow.RequestRepository.Get(r => r.Date.Year == request.Year && r.Date.Month == request.Month, null, "User");
            var users = await _uow.UserRepository.Get();
            var requestsByName = requests.GroupBy(r => r.User.Name).Select(u => new { Name = u.Key, Count = u.Count() }).ToList();
            
            foreach (var r in requestsByName)
            {
                result.Requests.Add(new KeyValuePair<string, int>(r.Name, r.Count));
            }

            foreach (var user in users)
            {
                if (!result.Requests.Where(r => r.Key == user.Name).Any())
                {
                    result.Requests.Add(new KeyValuePair<string, int>(user.Name, 0));
                }
            }

            return result;
        }
    }
}

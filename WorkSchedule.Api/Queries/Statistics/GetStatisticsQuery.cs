using MediatR;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Statistics
{
    public class GetStatisticsQuery : IRequest<StatisticsDto>
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}

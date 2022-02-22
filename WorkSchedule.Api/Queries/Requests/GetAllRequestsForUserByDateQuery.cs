using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSchedule.Api.Dtos;

namespace WorkSchedule.Api.Queries.Requests
{
    public class GetAllRequestsForUserByDateQuery : IRequest<List<RequestWithUserDto>>
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}

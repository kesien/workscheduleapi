using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkSchedule.Api.Queries.Statistics;

namespace Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{year}/{month}")]
        public async Task<IActionResult> GetStatistics(int year, int month)
        {
            var result = await _mediator.Send(new GetStatisticsQuery { Year = year, Month = month });
            return Ok(result);
        }
    }
}

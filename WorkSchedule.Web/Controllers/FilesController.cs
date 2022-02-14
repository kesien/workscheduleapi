using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkSchedule.Api.Queries.Files;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var (fileName, file) = await _mediator.Send(new GetFileByFileNameQuery() { Id = id });
            var result = new FileContentResult(file, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = fileName
            };

            return result;
        }
    }
}
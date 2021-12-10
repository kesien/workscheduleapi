using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleMaker.Data;

namespace WorkScheduleMaker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FilesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> DownloadFile(Guid id)
        {
            var file = _unitOfWork.WordFileRepository.GetByID(id);
            if (file is null) 
            {
                return BadRequest($"File with id: {id} couldn't be found");
            }
            
            var bytes = await System.IO.File.ReadAllBytesAsync(Path.Combine(file.FilePath, file.FileName));
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", file.FileName);
        }
    }
}
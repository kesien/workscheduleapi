using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleMaker.Data;
using WorkScheduleMaker.Services;

namespace WorkScheduleMaker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDropboxService _dropbox;

        public FilesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var file = _unitOfWork.WordFileRepository.GetByID(id);
            if (file is null) 
            {
                return NotFound($"File with id: {id} couldn't be found");
            }
            
            var bytes = await _dropbox.GetFile($"/{file.FilePath}");
            if (bytes is null)
            {
                return NotFound();
            }
            var result = new FileContentResult(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = file.FileName
            };

            return result;
        }
    }
}
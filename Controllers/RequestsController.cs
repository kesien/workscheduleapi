using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;
using WorkScheduleMaker.Services;

namespace WorkScheduleMaker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        public RequestsController(UserManager<User> userManager, IMapper mapper, IRequestService requestService)
        {
            _mapper = mapper;
            _requestService = requestService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllRequestsForAllUsers()
        {
            var requests = await _requestService.GetAllRequests();
            var requestsListDto = _mapper.Map<List<RequestWithUserDto>>(requests);
            return Ok(requestsListDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(RequestDto requestDto)
        {
            var userIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = userIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _requestService.CreateRequest(userId, requestDto);
            if (result is null)
            {
                return BadRequest($"You can't create a request on date: {requestDto.Date.ToString("yyyy-MM-dd")}");
            }
            var dto = _mapper.Map<RequestDto>(result);
            return Ok(dto);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetAllRequestsForUserByDate([FromQuery] string userid, [FromQuery] int year, [FromQuery] int month)
        {
            var requests = await _requestService.GetAllRequestsForUserByDate(userid, year, month);
            if (requests is null)
            {
                return BadRequest();
            }

            var requestDto = _mapper.Map<List<RequestWithUserDto>>(requests);

            return Ok(requestDto);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllRequestsForUser(string userId) 
        {
            var requests = await _requestService.GetAllRequestsForUser(userId);
            if (requests is null) 
            {
                return BadRequest();
            }
            var requestDto = _mapper.Map<List<RequestWithUserDto>>(requests);
            return Ok(requestDto);
        }


        [HttpDelete("{requestId}")]
        public async Task<IActionResult> DeleteRequest(Guid requestId)
        {
            var result = await _requestService.DeleteRequest(requestId);
            if (!result) 
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}

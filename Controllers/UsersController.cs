using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkScheduleMaker.Data;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;
using WorkScheduleMaker.Enums;

namespace WorkScheduleMaker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users.ToList();
            var userList = _mapper.Map<List<UserToListDto>>(users);
            return Ok(userList.OrderBy(user => user.Name));
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var userToShow = await _userManager.FindByIdAsync(id);
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = "";
            if (identity != null)
            {
                userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            if (string.IsNullOrEmpty(userId)) 
            {
                return Forbid();
            }
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            if (userToShow is null || (userId != id.ToString() && !roles.Contains("Administrator")))
            {
                return Forbid();
            }
            return Ok(_mapper.Map<UserToListDto>(userToShow));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserForRegisterDto userForRegisterDto)
        {
            var user = await _userManager.FindByNameAsync(userForRegisterDto.UserName);
            
            if (user is not null)
            {
                return BadRequest("Username already exists!");
            }
            var newUser = _mapper.Map<User>(userForRegisterDto);
            await _userManager.CreateAsync(newUser, userForRegisterDto.Password);
            if (userForRegisterDto.Role == UserRole.ADMIN)
            {
                await _userManager.AddToRoleAsync(newUser, "Administrator");
            } else
            {
                await _userManager.AddToRoleAsync(newUser, "User");
            }
            var newUserDto = _mapper.Map<UserToListDto>(newUser);
            return CreatedAtAction(nameof(GetUser), new { Id = newUser.Id}, newUserDto);
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, UserForUpdateDto userForUpdateDto)
        {
            var userToChange = await _userManager.FindByIdAsync(id);
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var url = HttpContext.Request.
            var userId = "";
            if (identity != null)
            {
                userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            if (userToChange is null || (userId != id.ToString() && !roles.Contains("Administrator")))
            {
                return Forbid();
            }

            if (!string.IsNullOrEmpty(userForUpdateDto.Password) && !string.IsNullOrWhiteSpace(userForUpdateDto.Password))
            {
                var passwordHash = _userManager.PasswordHasher.HashPassword(userToChange, userForUpdateDto.Password);
                userToChange.PasswordHash = passwordHash;
            }
            userToChange.UserName = userForUpdateDto.UserName ?? userToChange.UserName;
            if (userForUpdateDto.Name is not null && userForUpdateDto.Name != userToChange.Name)
            {
                userToChange.Name = userForUpdateDto.Name;
                await UpdateSummaries(userToChange.Id, userForUpdateDto.Name);
            }
            if (roles.Contains("Administrator"))
            {
                userToChange.Role = userForUpdateDto.Role ?? userToChange.Role;
            }
            await _userManager.UpdateAsync(userToChange);
            return NoContent();
        }

        private async Task UpdateSummaries(string id, string name)
        {
            var summaries = _unitOfWork.SummaryRepository.Get(summary => summary.UserId == id);
            foreach (var summary in summaries)
            {
                summary.Name = name;
            }
            _unitOfWork.Save();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return BadRequest();
            }
            await _userManager.DeleteAsync(user);
            var summaries = _unitOfWork.SummaryRepository.Get(summary => summary.UserId == id);
            foreach (var summary in summaries)
            {
                _unitOfWork.SummaryRepository.Delete(summary);
            }
            _unitOfWork.Save();
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkScheduleMaker.Dtos;
using WorkScheduleMaker.Entities;

namespace WorkScheduleMaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private SymmetricSecurityKey _key;

        public AuthController(SignInManager<User> signInManager, IConfiguration config, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWTSettings:Secret").Value));
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Username);
            if (user is null)
            {
                return Unauthorized("Invalid username or password!");
            }
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);
            if (signInResult.Succeeded)
            {
                var token = await GenerateToken(user);
                return Ok(new
                {
                    Token = token
                });
            }
            return Unauthorized("Invalid username or password!");
        }

        private async Task<string> GenerateToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

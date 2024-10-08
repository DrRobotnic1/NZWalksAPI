using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase   
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRespository tokenRespository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRespository tokenRespository)
        {
            _userManager = userManager; 
            this.tokenRespository = tokenRespository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };

            var identityResult =  await _userManager.CreateAsync(identityUser,registerRequestDto.Password);

            if (identityResult.Succeeded) {

                
                    return Ok("User was registered! Please login");

                 
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                
                var checkResult = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);

                if(checkResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var jwtToken = tokenRespository.CreateJWTToken(user, roles.ToList());
                    var response = new LoginResponseDto { 
                       JwtToken = jwtToken,
                    };
                }
            }
            return BadRequest("Username or password incorrect");

        }
    }
}

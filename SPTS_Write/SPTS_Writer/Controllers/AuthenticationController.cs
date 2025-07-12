using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Eventbus.ViewChanges;
using SPTS_Writer.Services;
using SPTS_Writer.Services.Abstraction;
using SPTS_Writer.Utils;
using System;
using System.Threading.Tasks;

namespace SPTS_Writer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Authen _authenService;
        private readonly IConfiguration _configuration;
        private readonly UserView _userView;  
        public AuthenticationController(Authen authenService, IConfiguration configuration,UserView userView)
        {
            _authenService = authenService;
            _configuration = configuration;
            _userView = userView;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var user = await _authenService.Login(loginRequest);
                var accessToken = JwtTokenHelper.GenerateAccessToken(user, _configuration);
                return Ok(new
                {
                    access_token = accessToken,
                    username = user.Name,
                    role = user.Role ?? "Student",
                    email = user.Email,
                    userId = user.Id
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                var user = await _authenService.Register(registerRequest);
                var accessToken = JwtTokenHelper.GenerateAccessToken(user, _configuration);
               /* await _userView.SyncUserSnapshotWithUsersAsync();*/
                return Ok(new
                {
                    access_token = accessToken,
                    username = user.Name,
                    role = user.Role ?? "Student",
                    email = user.Email,
                    userId = user.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

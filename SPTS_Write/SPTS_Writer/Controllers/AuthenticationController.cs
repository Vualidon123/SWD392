using Microsoft.AspNetCore.Mvc;
using SPTS_Writer.Entities;
using SPTS_Writer.Services;
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

        public AuthenticationController(Authen authenService, IConfiguration configuration)
        {
            _authenService = authenService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var user = await _authenService.Login(loginRequest);
                var accessToken = JwtTokenHelper.GenerateAccessToken(user, _configuration);
                return Ok(new { access_token = accessToken });
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
                return Ok(new { access_token = accessToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

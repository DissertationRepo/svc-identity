using IdentityService.Api.Models;
using IdentityService.Application.AbstractServices;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace IdentityService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthService _loginService;
        private readonly IMapper _mapper;
        public AuthController(IAuthService loginService, IMapper mapper)
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Token([FromBody] Login request)
        {
            var loginCommand = _mapper.Map<Application.Models.Login>(request);

            var loginResponse = await _loginService.Login(loginCommand);

            return Ok(loginResponse);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] Logout request)
        {
            var logoutCommand = _mapper.Map<Application.Models.Logout>(request);

            await _loginService.Logout(logoutCommand);
            
            return Ok("Logout was succesful!");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register request)
        {
            var registerCommand = _mapper.Map<Application.Models.Register>(request);

            var registerResponse = await _loginService.Register(registerCommand);

            if (registerResponse == true)
            {
                return Ok("Registration was succesful!");
            }
            return BadRequest("A user with this email already exists");
        }

        [HttpPost("refresh")]
        public async Task<Application.Models.RefreshResponse> Refresh([FromBody] Refresh request)
        {
            var refreshCommand = _mapper.Map<Application.Models.Refresh>(request);

            var refreshResponse = await _loginService.Refresh(refreshCommand);

            return refreshResponse;
        }
    }
}
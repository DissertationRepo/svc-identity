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
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;
        public AuthController(ILoginService loginService, IMapper mapper)
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Token([FromBody] Login request)
        {
            var loginCommand = _mapper.Map<Application.Models.Login>(request);

            var accessToken = _loginService.Login(loginCommand);

            return Ok(accessToken);
        }
    }
}
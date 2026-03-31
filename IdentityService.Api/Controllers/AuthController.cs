using IdentityService.Api.Models;
using IdentityService.Application.AbstractServices;
using IdentityService.Application.Common;
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
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var loginCommand = _mapper.Map<Application.Models.Login>(request);

            var loginResponse = await _loginService.Login(loginCommand);

            if (loginResponse.IsFailure)
            {
                return ToActionResult(loginResponse);
            }

            return Ok(loginResponse.Value);
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

            if (registerResponse.IsSuccess)
            {
                return Ok("Registration was succesful!");
            }

            return ToActionResult(registerResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] Refresh request)
        {
            var refreshCommand = _mapper.Map<Application.Models.Refresh>(request);

            var refreshResponse = await _loginService.Refresh(refreshCommand);

            if (refreshResponse.IsFailure)
            {
                return ToActionResult(refreshResponse);
            }

            return Ok(refreshResponse.Value);
        }

        private ObjectResult ToActionResult(Result result)
        {
            var statusCode = result.Error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            var title = statusCode switch
            {
                StatusCodes.Status400BadRequest => "Bad Request",
                StatusCodes.Status401Unauthorized => "Unauthorized",
                StatusCodes.Status404NotFound => "Not Found",
                StatusCodes.Status409Conflict => "Conflict",
                _ => "Server Error"
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = result.Error.Description,
                Type = $"https://httpstatuses.com/{statusCode}"
            };

            problemDetails.Extensions["code"] = result.Error.Code;
            problemDetails.Extensions["traceId"] = HttpContext.TraceIdentifier;

            return StatusCode(statusCode, problemDetails);
        }
    }
}
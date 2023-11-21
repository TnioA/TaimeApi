using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taime.Application.Services;
using Taime.Application.Helpers;
using Taime.Application.Contracts.Auth;

namespace Taime.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("login")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Login([FromQuery] LoginRequest request)
        {
            var response = await _userService.Login(request);
            return HttpHelper.Convert(response);
        }

        [HttpGet("refresh")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Refresh([FromQuery] RefreshRequest request)
        {
            var response = await _userService.RefreshToken(request);
            return HttpHelper.Convert(response);
        }
    }
}
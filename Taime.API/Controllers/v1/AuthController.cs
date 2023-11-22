using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taime.Application.Services;
using Taime.Application.Helpers;
using Taime.Application.Contracts.Auth;
using Taime.Application.Contracts.Shared;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Taime.Application.Constants;

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
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<TokenResponse>)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> Login([FromQuery] LoginRequest request)
        {
            var response = await _userService.Login(request);
            return HttpHelper.Convert(response);
        }

        [Authorize(Roles = AuthConstants.AUTH_REFRESH_ROLE)]
        [HttpGet("refresh")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<TokenResponse>)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> Refresh()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _userService.RefreshToken(userId);
            if (response == null)
                return new UnauthorizedObjectResult(response);

            return HttpHelper.Convert(response);
        }
    }
}
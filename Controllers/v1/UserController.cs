using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaimeApi.Data.MySql.Entities;
using TaimeApi.Services;
using TaimeApi.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace TaimeApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtem todos os usuários
        /// </summary>
        /// <remarks>
        /// {}
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno dos usuários</returns>
        [HttpGet("getall")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAll();
            return HttpHelper.Convert(response);
        }

        [HttpGet("login")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            var response = await _userService.Login(email, password);
            return HttpHelper.Convert(response);
        }

        [HttpPost("create")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Create([FromBody] UserEntity request)
        {
            var response = await _userService.Create(request);
            return HttpHelper.Convert(response);
        }

        [HttpPost("remove")]
        [Authorize(Roles = "Admin")] // deve ser adicionado as regras aceitas separados por virgula exemplo [Authorize(Roles = "Regra1,Regra2,Regra3")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Remove([FromQuery] int id)
        {
            var response = await _userService.Remove(id);
            return HttpHelper.Convert(response);
        }
    }
}
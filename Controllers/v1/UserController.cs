using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaimeApi.Data.MySql.Entities;
using TaimeApi.Services;

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

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var response = await _userService.GetById(id);
            return HttpHelper.Convert(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserEntity request)
        {
            var response = await _userService.Create(request);
            return HttpHelper.Convert(response);
        }
    }
}
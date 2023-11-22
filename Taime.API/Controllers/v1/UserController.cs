using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Services;
using Taime.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Taime.Application.Constants;

namespace Taime.API.Controllers.v1
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
        /// Obt�m todos os usu�rios
        /// </summary>
        /// <remarks>
        /// Exemplo de requisi��o para obter os usu�rios
        ///
        ///     Request:
        ///     GET v1/user
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno dos usu�rios</returns>
        [HttpGet()]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = AuthConstants.AUTH_ADMIN_ROLE)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAll();
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Obt�m um usu�rio por id
        /// </summary>
        /// <remarks>
        /// Exemplo de requisi��o para obter um usu�rio por id
        ///
        ///     Request:
        ///     GET v1/user/123
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno do usu�rio</returns>
        [HttpGet("{id}")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = AuthConstants.AUTH_ADMIN_ROLE)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _userService.GetById(id);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Cria um novo usu�rio
        /// </summary>
        /// <remarks>
        /// Exemplo de requisi��o para criar um usu�rio
        ///
        ///     Request:
        ///     POST v1/user
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno de sucesso</returns>
        [HttpPost()]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Create([FromBody] UserEntity request)
        {
            var response = await _userService.Create(request);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Remove um usu�rio existente
        /// </summary>
        /// <remarks>
        /// Exemplo de requisi��o para remover um usu�rio
        ///
        ///     Request:
        ///     DELETE v1/user/123
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno de sucesso</returns>
        [HttpDelete("{id}")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = AuthConstants.AUTH_ADMIN_ROLE)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var response = await _userService.Remove(id);
            return HttpHelper.Convert(response);
        }
    }
}
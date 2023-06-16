using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Services;
using Taime.Application.Helpers;
using System.Threading.Tasks;

namespace Taime.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AppointmentController : ControllerBase
    {
        public readonly AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Obt�m os apontamentos por usu�rio
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// Exemplo de requisi��o para obter os apontamentos por usu�rio
        ///
        ///     Request:
        ///     GET v1/appointment/getbyuser
        /// </remarks>
        /// <response code="200">Apontamentos por usu�rio retornados com sucesso.</response>
        /// <response code="400">Erros de valida��o.</response>
        [HttpGet("getbyuser")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetByUser([FromQuery] int userId)
        {
            var response = await _appointmentService.GetByUser(userId);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Cria um novo apontamento
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// Exemplo de requisi��o para criar um novo apontamento
        ///
        ///     Request:
        ///     PUT v1/appointment/create
        ///     {
        ///         "description": "Apontamento de teste",
        ///         "userId": "AQd9JDOqAOWBg4y"
        ///     }    
        /// </remarks>
        /// <response code="200">Apontamento criado com sucesso.</response>
        /// <response code="400">Erros de valida��o.</response>
        [HttpPost("create")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Create([FromBody] AppointmentEntity request)
        {
            var response = await _appointmentService.Create(request);
            return HttpHelper.Convert(response);
        }
    }
}
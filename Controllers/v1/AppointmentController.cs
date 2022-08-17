using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaimeApi.Data.MySql.Entities;
using TaimeApi.Services;
using TaimeApi.Helpers;

namespace TaimeApi.Controllers.v1
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

        [HttpGet("getbyuser")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetByUser([FromQuery] int userId)
        {
            var response = await _appointmentService.GetByUser(userId);
            return HttpHelper.Convert(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AppointmentEntity request)
        {
            var response = await _appointmentService.Create(request);
            return HttpHelper.Convert(response);
        }
    }
}
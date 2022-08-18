using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaimeApi.Helpers;
using TaimeApi.Utils.Services;

namespace TaimeApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class HelthCheckController : ControllerBase
    {
        public HelthCheckController() { }

        [HttpGet("check")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public IActionResult Check()
        {
            var response = new ResultData(true);
            return HttpHelper.Convert(response);
        }
    }
}
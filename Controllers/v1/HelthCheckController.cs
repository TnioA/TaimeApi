using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaimeApi.Data.MySql.Entities;
using TaimeApi.Services;
using TaimeApi.Utils.Services;

namespace TaimeApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class HelthCheckController : ControllerBase
    {
        public HelthCheckController() { }

        /// <summary>
        /// Obtem todos os usuários
        /// </summary>
        /// <remarks>
        /// {}
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno dos usuários</returns>
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
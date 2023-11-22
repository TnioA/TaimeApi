using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taime.Application.Services;
using Taime.Application.Helpers;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Constants;
using Taime.Application.Contracts.Shared;
using Taime.Application.Contracts.Brand;

namespace Taime.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class BrandController : ControllerBase
    {
        public readonly BrandService _brandService;

        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }

        /// <summary>
        /// Obtém todas as marcas
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter as marcas
        ///
        ///     Request:
        ///     GET v1/brand
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno das marcas</returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<List<BrandResponse>>)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> Get()
        {
            var response = await _brandService.GetAll();
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Obtém uma marca por id
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter uma marca por id
        ///
        ///     Request:
        ///     GET v1/brand/123
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno da marca</returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<BrandResponse>)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _brandService.GetById(id);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Cria uma nova marca
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para criar uma marca
        ///
        ///     Request:
        ///     POST v1/brand
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno de sucesso</returns>
        [HttpPost()]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = AuthConstants.AUTH_ADMIN_ROLE)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> Create([FromBody] BrandRequest request)
        {
            var response = await _brandService.Create(request);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Remove uma marca existente
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para remover uma marca
        ///
        ///     Request:
        ///     DELETE v1/brand/123
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno de sucesso</returns>
        [HttpDelete("{id}")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = AuthConstants.AUTH_ADMIN_ROLE)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var response = await _brandService.Remove(id);
            return HttpHelper.Convert(response);
        }
    }
}
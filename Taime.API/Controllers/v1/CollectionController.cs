using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taime.Application.Services;
using Taime.Application.Helpers;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Constants;

namespace Taime.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class CollectionController : ControllerBase
    {
        public readonly CollectionService _collectionService;

        public CollectionController(CollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        /// <summary>
        /// Obtém todas as coleções
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter as coleções
        ///
        ///     Request:
        ///     GET v1/collection
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno das coleções</returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Get()
        {
            var response = await _collectionService.GetAll();
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Obtém uma coleção por id
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter uma coleção por id
        ///
        ///     Request:
        ///     GET v1/collection/123
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno da coleção</returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _collectionService.GetById(id);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Cria uma nova coleção
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para criar uma coleção
        ///
        ///     Request:
        ///     POST v1/collection
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno de sucesso</returns>
        [HttpPost()]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = AuthConstants.AUTH_ADMIN_ROLE)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Create([FromBody] CollectionEntity request)
        {
            var response = await _collectionService.Create(request);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Remove uma coleção existente
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para remover uma coleção
        ///
        ///     Request:
        ///     DELETE v1/collection/123
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
            var response = await _collectionService.Remove(id);
            return HttpHelper.Convert(response);
        }
    }
}
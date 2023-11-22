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
    public class CategoryController : ControllerBase
    {
        public readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Obtém todas as categorias
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter as categorias
        ///
        ///     Request:
        ///     GET v1/category
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno das categorias</returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Get()
        {
            var response = await _categoryService.GetAll();
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Obtém uma categoria por id
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter uma categoria por id
        ///
        ///     Request:
        ///     GET v1/category/123
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno da categoria</returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _categoryService.GetById(id);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Cria uma nova categoria
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para criar uma categoria
        ///
        ///     Request:
        ///     POST v1/category
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno de sucesso</returns>
        [HttpPost()]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = AuthConstants.AUTH_ADMIN_ROLE)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Create([FromBody] CategoryEntity request)
        {
            var response = await _categoryService.Create(request);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Remove uma categoria existente
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para remover uma categoria
        ///
        ///     Request:
        ///     DELETE v1/category/123
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
            var response = await _categoryService.Remove(id);
            return HttpHelper.Convert(response);
        }
    }
}
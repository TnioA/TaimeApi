using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taime.Application.Services;
using Taime.Application.Helpers;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Constants;
using Taime.Application.Contracts.Shared;
using Taime.Application.Contracts.Product;

namespace Taime.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        public readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Obtém todos os produtos
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter os produtos
        ///
        ///     Request:
        ///     GET v1/product
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno dos produtos</returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<List<ProductResponse>>)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> Get()
        {
            var response = await _productService.GetAll();
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Obtém um produto por id
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter um produto por id
        ///
        ///     Request:
        ///     GET v1/product/123
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno do produto</returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<ProductResponse>)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _productService.GetById(id);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Cria um novo produto
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para criar um produto
        ///
        ///     Request:
        ///     POST v1/product
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno de sucesso</returns>
        [HttpPost()]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = AuthConstants.AUTH_ADMIN_ROLE)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            var response = await _productService.Create(request);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Remove um produto existente
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para remover um produto
        ///
        ///     Request:
        ///     DELETE v1/product/123
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
            var response = await _productService.Remove(id);
            return HttpHelper.Convert(response);
        }
    }
}
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taime.Application.Services;
using Taime.Application.Helpers;
using Taime.Application.Data.MySql.Entities;

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
        /// Obtem todos os produtos
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter os produtos.
        ///
        ///     Request:
        ///     GET v1/products
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno dos produtos</returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Get()
        {
            var response = await _productService.GetAll();
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Obtem um produto por id
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter um produto por id
        ///
        ///     Request:
        ///     GET v1/products/1
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno do produto</returns>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _productService.GetById(id);
            return HttpHelper.Convert(response);
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")] // deve ser adicionado as regras aceitas separados por virgula exemplo [Authorize(Roles = "Regra1,Regra2,Regra3")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Create([FromBody] ProductEntity request)
        {
            var response = await _productService.Create(request);
            return HttpHelper.Convert(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // deve ser adicionado as regras aceitas separados por virgula exemplo [Authorize(Roles = "Regra1,Regra2,Regra3")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var response = await _productService.Remove(id);
            return HttpHelper.Convert(response);
        }
    }
}
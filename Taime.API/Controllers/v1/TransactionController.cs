using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taime.Application.Services;
using Taime.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Taime.Application.Constants;
using Taime.Application.Contracts.Shared;
using Taime.Application.Contracts.Transaction;
using System.Security.Claims;

namespace Taime.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class TransactionController : ControllerBase
    {
        public readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Obtém todas as transações
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter as transações
        ///
        ///     Request:
        ///     GET v1/transaction
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno das transações</returns>
        [HttpGet()]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = AuthConstants.AUTH_ADMIN_ROLE)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<List<TransactionResponse>>)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> GetAll()
        {
            var response = await _transactionService.GetAll();
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Obtém uma transação por id
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para obter uma transação por id
        ///
        ///     Request:
        ///     GET v1/transaction/123
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno do usuário</returns>
        [HttpGet("{id}")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize(Roles = AuthConstants.AUTH_ADMIN_ROLE)]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData<TransactionResponse>)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _transactionService.GetById(id);
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Cria uma nova transação
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição para criar uma transação
        ///
        ///     Request:
        ///     POST v1/transaction
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno de sucesso</returns>
        [HttpPost()]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ResultData)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorData)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(ErrorData))]
        public async Task<IActionResult> Create([FromBody] TransactionRequest request)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _transactionService.Create(request, userId);
            return HttpHelper.Convert(response);
        }
    }
}
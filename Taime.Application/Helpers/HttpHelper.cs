using Microsoft.AspNetCore.Mvc;
using System.Net;
using Taime.Application.Contracts.Shared;

namespace Taime.Application.Helpers
{
    public static class HttpHelper
    {
        public static ActionResult Convert(ResultData result)
        {
            if(result == null)
                throw new ArgumentException("Result cannot be null.", "result");

            if(result.Success)
                return new ObjectResult(result) { StatusCode = (int)HttpStatusCode.OK };

            return new BadRequestObjectResult(result);
        }
    }
}
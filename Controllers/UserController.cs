using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaimeApi.Models;
using TaimeApi.Services;

namespace TaimeApi.Controllers
{
    [ApiController]
    [Route("v1/user")]
    public class UserController: ControllerBase
    {
        public readonly UserService _userService;

        public UserController([FromServices] UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<UserModel>>> GetAll()
        {
            var response = await _userService.GetAll();
            return response;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<UserModel>> GetById([FromQuery] int id)
        {
            var response = await _userService.GetById(id);
            return response;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<UserModel>> Create([FromBody] UserModel request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.Create(request);
            return response;
        }
    }
}
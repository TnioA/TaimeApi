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

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAll();
            return HttpHelper.Convert(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var response = await _userService.GetById(id);
            return HttpHelper.Convert(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserModel request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.Create(request);
            return HttpHelper.Convert(response);
        }
    }
}
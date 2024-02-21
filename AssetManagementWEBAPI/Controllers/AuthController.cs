using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWEBAPI.Controllers
{
    [ApiController]
    [Route ("user/")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login/")]
        public ActionResult<string> UserLogin([FromBody] User user)
        {
            string token = _userService.Login(user);
            if(token == null)
            {
                return BadRequest ("User credentials are not valid!");
            }
            return Ok (token);
        }
    }
}

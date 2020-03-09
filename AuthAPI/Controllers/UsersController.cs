using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthAPI.Models;
using AuthAPI.Services.Interfaces;

namespace AuthAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult  Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if(user == null)
                return BadRequest(new { message = "Username or password incorrect"});
            
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var user = _userService.GetAll();
            return Ok(user);
        }

    }
}
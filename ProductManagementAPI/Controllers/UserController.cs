using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Models;
using ProductManagementAPI.Services;
using ProductManagementAPI.Services.Interfaces;

namespace ProductManagementAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDTO userDto)
        {
            var response = _userService.RegisterUser(userDto);
            if (response == null) return BadRequest("Email already exists.");

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDTO userDto)
        {
            var response = _userService.LoginUser(userDto);
            if (response == null) return Unauthorized("Invalid credentials.");

            return Ok(response);
        }
    }
}

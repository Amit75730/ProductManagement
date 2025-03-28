using Microsoft.AspNetCore.Mvc;
using ProductManagementMVC.Models;
using ProductManagementMVC.Services.Interfaces;
using System.Threading.Tasks;

namespace ProductManagementMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userDto)
        {
            var token = await _userService.LoginUser(userDto);
            if (token == null)
            {
                ViewBag.Error = "Invalid credentials.";
                return View();
            }

            HttpContext.Session.SetString("Token", token);
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO userDto)
        {
            var result = await _userService.RegisterUser(userDto);
            if (result == null)
            {
                ViewBag.Error = "Email already exists.";
                return View();
            }

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login");
        }
    }
}

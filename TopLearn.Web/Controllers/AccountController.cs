using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Register")]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);

            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "این ایمیل قبلا ثبت شده است.");
                return View(register);
            }

            if (_userService.IsExistEmail(FixedText.FixedEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "این ایمیل قبلا ثبت شده است.");
                return View(register);
            }

            //TODO: Register User

            return View();
        }
    }
}

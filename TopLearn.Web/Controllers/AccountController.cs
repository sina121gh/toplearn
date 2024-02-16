using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #region Register
        [Route("Register")]
        public IActionResult Register() => View();

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);

            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "این نام کاربری قبلا ثبت شده است.");
                return View(register);
            }

            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "این ایمیل قبلا ثبت شده است.");
                return View(register);
            }

            string salt = PasswordHelper.GenerateSalt();
            var user = new User()
            {
                UserName = register.UserName,
                Email = FixedText.FixEmail(register.Email),
                Salt = salt,
                Password = PasswordHelper.HashPassword(register.Password, salt),
                RegisterDate = DateTime.Now,
                ActiveCode = MyGenerator.GenerateCode(),
                IsActive = false,
                Avatar = "DefaultAvatar.png",
            };

            _userService.AddUser(user);

            //TODO: Send Email Activation

            return View("SuccessRegister", user);
        }
        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login() => View();

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            var user = _userService.LoginUser(login);

            if (user!= null)
            {
                if (user.IsActive)
                {
                    ViewBag.IsSuccess = true;
                    return View();
                }
                    
                else
                    ModelState.AddModelError("Email", "حساب کاربری شما تایید نشده است");
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View();
        }
        #endregion
    }
}

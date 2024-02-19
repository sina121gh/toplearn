﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopLearn.Convertors;
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
        private readonly IViewRenderService _viewRender;

        public AccountController(IUserService userService, IViewRenderService viewRender)
        {
            _userService = userService;
            _viewRender = viewRender;
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
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe,
                    };

                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    return View();
                }
                    
                else
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View();
        }
        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }
        #endregion

        #region Active Account

        [Route("ActiveAccount/{activeCode}")]
        public IActionResult ActiveAccount(string activeCode)
        {
            ViewBag.IsActive = _userService.ActiveAccount(activeCode);

            return View();
        }

        #endregion
    }
}

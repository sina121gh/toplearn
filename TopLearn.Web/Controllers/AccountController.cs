using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopLearn.Convertors;
using TopLearn.Core.Convertors;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;
using TopLearn.Senders;

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

            #region Send Activation Email

            string emailBody = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعال سازی حساب", emailBody);

            #endregion

            return View("SuccessRegister", user);
        }
        #endregion

        #region Login
        [Route("Login")]
        public IActionResult Login(bool EditProfile = false)
        {
            ViewBag.EditProfile = EditProfile;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            User user = _userService.GetUserForLogin(login);

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
            ModelState.AddModelError("Email", "اطلاعات وارد شده صحیح نمیباشد");
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

        #region Forgot Password
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
                return View(forgot);

            User user = _userService.GetUserByEmail(FixedText.FixEmail(forgot.Email));
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری یافت نشد");
                return View(forgot);
            }

            string emailBody = _viewRender.RenderToStringAsync("_ForgotPasswordEmail", user);

            SendEmail.Send(forgot.Email, "بازیابی کلمه عبور", emailBody);
            ViewBag.IsSuccess = true;

            return View();
        }
        #endregion

        #region Reset Password
        [Route("/ResetPassword/{activeCode}")]
        public IActionResult ResetPassword(string activeCode)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = activeCode
            });
        }

        [HttpPost]
        [Route("/ResetPassword/{activeCode}")]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
                return View(reset);

            User user = _userService.GetUserByActiveCode(reset.ActiveCode);
            if (user == null)
                NotFound();

            user.Salt = PasswordHelper.GenerateSalt();
            user.Password = PasswordHelper.HashPassword(reset.Password, user.Salt);
            user.ActiveCode = MyGenerator.GenerateCode();
            _userService.UpdateUser(user);

            ViewBag.IsSuccess = true;

            return View();
        }

        #endregion
    }
}

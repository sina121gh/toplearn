using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index() => View(_userService.GetUserInformation(User.Identity.Name));

        #region Edit Profile

        [Route("/UserPanel/EditProfile")]
        public IActionResult EditProfile() => View(_userService.GetUserForEdit(User.Identity.Name));

        [HttpPost]
        [Route("/UserPanel/EditProfile")]
        public IActionResult EditProfile(EditProfileViewModel profile)
        {
            if (!ModelState.IsValid)
                return View(profile);

            User user = _userService.GetUserByUserName(User.Identity.Name);

            #region Data Duplication Check

            string currentUserName = user.UserName;
            string currentEmail = user.Email;

            
            if (profile.UserName != currentUserName)
                if (_userService.IsExistUserName(profile.UserName))
                {
                    ModelState.AddModelError("UserName", "این نام کاربری قبلا ثبت شده است");
                    return View(profile);
                }

            if (profile.Email != currentEmail)
                if (_userService.IsExistEmail(profile.Email))
                {
                    if (_userService.IsExistUserName(profile.UserName))
                    {
                        ModelState.AddModelError("Email", "این ایمیل قبلا ثبت شده است");
                        return View(profile);
                    }
                }
            #endregion

            _userService.EditProfile(currentUserName, profile);

            // Logout User
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return Redirect("/Login?EditProfile=true");
        }

        #endregion

        #region Change Password
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword() => View();

        [HttpPost]
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel changePass)
        {
            if (!ModelState.IsValid)
                return View(changePass);

            string userName = User.Identity.Name;
            string userPassword = _userService.GetUserPassword(userName);

            if (!PasswordHelper.VerifyPassword(changePass.CurrentPassword, userPassword))
            {
                ModelState.AddModelError("CurrentPassword", "کلمه عبور فعلی صحیح نمیباشد");
                return View(changePass);
            }

            try
            {
                _userService.ChangeUserSalt(userName);
                _userService.ChangeUserPassword(userName, changePass.Password);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            ViewBag.IsSuccess = true;
            return View();
        }

        #endregion

    }
}

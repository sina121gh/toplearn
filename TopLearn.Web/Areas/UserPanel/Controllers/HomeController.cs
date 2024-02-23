using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs;
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

        [Route("/UserPanel/EditProfile")]
        public IActionResult EditProfile() => View(_userService.GetUserForEdit(User.Identity.Name));

        [HttpPost]
        [Route("/UserPanel/EditProfile")]
        public IActionResult EditProfile(EditProfileViewModel profile)
        {
            if (!ModelState.IsValid)
                return View(profile);

            User user = _userService.GetUserByUserName(User.Identity.Name);
            string currentUserName = user.UserName;
            string currentEmail = user.Email;

            #region Data Duplication Check
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

    }
}

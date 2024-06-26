﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace TopLearn.Web.Pages.Admin.Users
{
    [PermissionChecker("حذف کاربر")]
    public class DeleteUserModel : PageModel
    {

        private readonly IUserService _userService;

        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet(int userId)
        {
            _userService.DeleteUser(userId);
            //return Content(JsonSerializer.Serialize(_userService.GetUsers()));
            return Redirect("/admin/users/");
        }
    }
}

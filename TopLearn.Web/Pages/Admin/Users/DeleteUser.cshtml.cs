using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace TopLearn.Web.Pages.Admin.Users
{
    [IgnoreAntiforgeryToken]
    [PermissionChecker("حذف کاربر")]
    public class DeleteUserModel : PageModel
    {

        private readonly IUserService _userService;

        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnDelete(int userId)
        {
            var success = _userService.DeleteUser(userId);
            //return Content(JsonSerializer.Serialize(_userService.GetUsers()));
            return new JsonResult(new { success = success });
        }
    }
}

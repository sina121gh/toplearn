using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        #region Dependency Injection

        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion


        public UsersForAdminViewModel UsersViewModel { get; set; }

        public void OnGet()
        {
            UsersViewModel = _userService.GetUsers();
        }
    }
}

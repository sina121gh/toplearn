using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class UsersListModel : PageModel
    {
        #region Dependency Injection

        private readonly IUserService _userService;

        public UsersListModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion


        public UsersForAdminViewModel UsersViewModel { get; set; }

        public void OnGet(int take = 10, int pageId = 1, string userName = "", string email = "")
        {
            UsersViewModel = _userService.GetUsers(take ,pageId, userName, email);
        }
    }
}

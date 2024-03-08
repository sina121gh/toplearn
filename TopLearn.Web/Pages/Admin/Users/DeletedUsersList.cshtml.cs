using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class DeletedUsersListModel : PageModel
    {
        #region Dependency Injection

        private readonly IUserService _userService;

        public DeletedUsersListModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion


        public UsersForAdminViewModel UsersViewModel { get; set; }

        public void OnGet(int take = 10, int pageId = 1, string userName = "", string email = "")
        {
            UsersViewModel = _userService.GetDeletedUsers(take, pageId, userName, email);
        }
    }
}

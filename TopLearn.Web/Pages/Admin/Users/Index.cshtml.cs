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

        [BindProperty]
        public UsersForAdminViewModel UsersViewModel { get; set; }

        //[BindProperty]
        //public string FilterByUserName { get; set; }

        //[BindProperty]
        //public string FilterByEmail { get; set; }

        //[BindProperty]
        //public int Take { get; set; }

        public void OnGet(int take = 10, int pageId = 1, string userName = "", string email = "")
        {
            //if (string.IsNullOrEmpty(userName))
            //    FilterByUserName = userName;

            //if (string.IsNullOrEmpty(email))
            //    FilterByEmail = email;

            //Take = take;

            UsersViewModel = _userService.GetUsers(take ,pageId,email, userName);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class CreateUserModel : PageModel
    {

        #region Dependency Injection

        private readonly IUserService _userService;
        private readonly IPermisionService _permisionService;

        public CreateUserModel(IUserService userService, IPermisionService permisionService)
        {
            _userService = userService;
            _permisionService = permisionService;
        }

        #endregion

        [BindProperty]
        public CreateUserViewModel? CreateUserViewModel { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(IEnumerable<int> SelectedRolesIds)
        {
            if (!ModelState.IsValid)
                return Page();

            #region Add Roles

            int userId = _userService.AddUserFromAdmin(CreateUserViewModel);

            #endregion

            #region Add Roles

            _permisionService.AddRolesToUser(SelectedRolesIds, userId);

            #endregion

            return Redirect("/admin/users-list/");
        }
    }
}

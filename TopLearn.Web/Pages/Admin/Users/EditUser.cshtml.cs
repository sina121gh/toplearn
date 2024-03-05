using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Users
{
    public class EditUserModel : PageModel
    {

        #region Dependency Injection

        private readonly IUserService _userService;
        private readonly IPermisionService _permisionService;

        public EditUserModel(IUserService userService, IPermisionService permisionService)
        {
            _userService = userService;
            _permisionService = permisionService;
        }

        #endregion

        [BindProperty]
        public EditUserViewModel EditUserViewModel { get; set; }

        public void OnGet(int userId)
        {
            EditUserViewModel = _userService.GetUserForEdit(userId);
        }

        public IActionResult OnPost(IEnumerable<int> SelectedRolesIds)
        {
            if (!ModelState.IsValid)
                return Page();

            _userService.EditUserFromAdmin(EditUserViewModel);
            _permisionService.EditUserRoles(EditUserViewModel.UserId, SelectedRolesIds);

            return Redirect("/admin/users-list/");
        }
    }
}

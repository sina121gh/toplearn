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

        public IEnumerable<Role> Roles { get; set; }

        public void OnGet()
        {
            Roles = _permisionService.GetRoles();
        }

        public IActionResult OnPost(IEnumerable<int> SelectedRolesIds)
        {
            return Page();
        }
    }
}

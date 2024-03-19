using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Roles
{
    public class EditRoleModel : PageModel
    {
        private readonly IPermisionService _permisionService;

        public EditRoleModel(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(int roleId)
        {
            Role = _permisionService.GetRoleById(roleId);
        }

        public IActionResult OnPost(IEnumerable<int> selectedPermissionsIds)
        {
            if (!ModelState.IsValid)
                return Page();

            _permisionService.UpdateRole(Role);
            _permisionService.UpdateRolePermissions(Role.Id, selectedPermissionsIds);

            return Redirect("/admin/roles/");
        }
    }
}

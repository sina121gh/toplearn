using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Security;

namespace TopLearn.Web.Pages.Admin.Roles
{
    [IgnoreAntiforgeryToken]
    [PermissionChecker("حذف نقش")]
    public class DeleteRoleModel : PageModel
    {
        private readonly IPermisionService _permisionService;

        public DeleteRoleModel(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        public IActionResult OnDelete(int roleId)
        {
            _permisionService.DeleteRole(roleId);
            return new JsonResult(new { success = true });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Roles
{
    public class DeleteRoleModel : PageModel
    {
        private readonly IPermisionService _permisionService;

        public DeleteRoleModel(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        public void OnGet(int roleId)
        {
            _permisionService.DeleteRole(roleId);
        }
    }
}

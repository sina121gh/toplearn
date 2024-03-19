using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Security;

namespace TopLearn.Web.Pages.Admin.Roles
{
    [PermissionChecker("مدیریت نقش ها")]
    public class IndexModel : PageModel
    {
        private readonly IPermisionService _permisionService;

        public IndexModel(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        public List<Role> RolesList { get; set; }

        public void OnGet()
        {
            RolesList = _permisionService.GetRoles().ToList();
        }
    }
}

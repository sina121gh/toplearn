using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Roles
{
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Roles
{
    public class CreateRoleModel : PageModel
    {
        private readonly IPermisionService _permisionService;

        public CreateRoleModel(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            int roleId = _permisionService.AddRole(Role);         
            
            //Todo Add Permission

            return Redirect("/admin/roles/");
        }
    }
}

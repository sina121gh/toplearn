using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin
{
    [PermissionChecker("پنل مدیریت")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

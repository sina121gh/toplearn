using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPermisionService _permisionService;

        public HomeController(IPermisionService permisionService)
        {
            _permisionService = permisionService;
        }

        public IActionResult Index() => View();


        [Authorize]
        public IActionResult Test() => View();


        [Route("/get-roles")]
        public JsonResult GetRoles()
        {
            var roles = _permisionService.GetRoles();
            return Json(roles);
        }

        [Route("/create-role")]
        public JsonResult CreateRole(Role role)
        {
            if (!ModelState.IsValid)
                return Json("Model validation failed", ModelState);

            _permisionService.AddRole(role);

            return Json("Role successfully added");
        }
    }
}

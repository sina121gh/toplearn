using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs;

namespace TopLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        [Route("Register")]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            //if (!ModelState.IsValid)
            //    return View(register);

            return View();
        }
    }
}

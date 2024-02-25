using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly TopLearnContext _context;
        private readonly IUserService _userService;

        public WalletController(TopLearnContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }


        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            return View(_userService.GetTransactions(User.Identity.Name));
        }
    }
}

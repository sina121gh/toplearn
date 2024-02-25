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


        [Route("UserPanel/Transactions")]
        public IActionResult Transactions()
        {
            return View(_userService.GetTransactions(User.Identity.Name));
        }

        [Route("UserPanel/ChargeWallet")]
        public IActionResult ChargeWallet() => View();

        [HttpPost]
        [Route("UserPanel/ChargeWallet")]
        public IActionResult ChargeWallet(ChargeWalletViewModel charge)
        {
            if (!ModelState.IsValid)
                return View(charge);

            //ToDo : Online Payment
            _userService.ChargeWallet(User.Identity.Name, charge.Amount, "شارژ حساب");

            return RedirectToAction("Transactions");
        }
    }
}

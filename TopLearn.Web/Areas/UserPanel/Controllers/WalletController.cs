using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sprache;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Wallet;
using Transaction = TopLearn.DataLayer.Entities.Wallet.Transaction;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly TopLearnContext _context;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;

        public WalletController(TopLearnContext context, IUserService userService, IPaymentService paymentService)
        {
            _context = context;
            _userService = userService;
            _paymentService = paymentService;
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
        public async Task<IActionResult> ChargeWallet(ChargeWalletViewModel charge)
        {
            if (!ModelState.IsValid)
                return View(charge);

            //ToDo : Online Payment
            int walletId = _userService.ChargeWallet(User.Identity.Name, charge.Amount, "شارژ حساب");

            #region Online Payment

            string authority = await _paymentService.PaymentRequest(charge.Amount, walletId, "شارژ حساب");

            if (authority == "")
                return NotFound();

            return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{authority}");
            #endregion
        }

        [Route("ValidatePayment/{transactionId}")]
        public async Task<IActionResult> ValidatePayment(int transactionId)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                Transaction? transaction = _userService.GetTransactionById(transactionId);
                int status = await _paymentService.ValidatePayment(transaction.Amount, authority);

                if (status == 100)
                {
                    transaction.IsPaid = true;
                    _userService.UpdateTransaction(transaction);
                    return Redirect("/UserPanel");
                }
            }

            return BadRequest();
        }
    }
}

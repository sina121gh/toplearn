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
        #region Dependency Injection

        private readonly TopLearnContext _context;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;

        public WalletController(TopLearnContext context, IUserService userService, IPaymentService paymentService)
        {
            _context = context;
            _userService = userService;
            _paymentService = paymentService;
        }

        #endregion

        [Route("UserPanel/Transactions")]
        public IActionResult Transactions() => View(_userService.GetTransactions(User.Identity.Name));

        #region Charge Wallet

        [Route("UserPanel/ChargeWallet")]
        public IActionResult ChargeWallet() => View();

        [HttpPost]
        [Route("UserPanel/ChargeWallet")]
        public async Task<IActionResult> ChargeWallet(ChargeWalletViewModel charge)
        {
            if (!ModelState.IsValid)
                return View(charge);

            int walletId = _userService.ChargeWallet(User.Identity.Name, charge.Amount, "شارژ حساب");

            #region Online Payment

            string authority = await _paymentService.PaymentRequest(charge.Amount, walletId, "شارژ حساب");

            if (authority == "")
                return NotFound();

            return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{authority}");
            #endregion
        }

        #endregion

        #region Validate Payment

        [Route("ValidatePayment/{transactionId}")]
        public async Task<IActionResult> ValidatePayment(int transactionId)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                Transaction? transaction = _userService.GetTransactionById(transactionId);
                var verification = await _paymentService.ValidatePayment(transaction.Amount, authority);

                // 100 = Ok Payment
                if (verification.Status == 100)
                {
                    transaction.IsSuccess = true;
                    _userService.UpdateTransaction(transaction);
                    ViewBag.IsSuccess = true;
                    ViewBag.Code = verification.RefId;
                }
            }

            return View();
        }

        #endregion
    }
}

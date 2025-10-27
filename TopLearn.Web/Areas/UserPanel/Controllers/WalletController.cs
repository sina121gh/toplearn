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

        private readonly string GatewayUrl = Environment.GetEnvironmentVariable("GATEWAY_URL_TO_PAY");

        public WalletController(TopLearnContext context, IUserService userService, IPaymentService paymentService)
        {
            _context = context;
            _userService = userService;
            _paymentService = paymentService;
        }

        #endregion

        [Route("user-panel/transactions")]
        public IActionResult Transactions() => View(_userService.GetTransactions(User.Identity.Name));

        #region Charge Wallet

        [Route("user-panel/charge-wallet")]
        public IActionResult ChargeWallet() => View();

        [HttpPost]
        [Route("user-panel/charge-wallet")]
        public async Task<IActionResult> ChargeWallet(ChargeWalletViewModel charge)
        {
            if (!ModelState.IsValid)
                return View(charge);

            int walletId = _userService.ChargeWallet(User.Identity.Name, charge.Amount, "شارژ حساب");

            #region Online Payment

            string authority = await _paymentService.PaymentRequestAsync(charge.Amount, walletId, "شارژ حساب");

            if (authority == "")
                return BadRequest();

            return Redirect($"{GatewayUrl}/{authority}");
            #endregion
        }

        #endregion

        #region Validate Payment

        [Route("validate-payment/{transactionId}")]
        public async Task<IActionResult> ValidatePayment(int transactionId)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                Transaction? transaction = _userService.GetTransactionById(transactionId);
                var verification = await _paymentService.ValidatePaymentAsync(transaction.Amount, authority);

                // 100 = Ok Payment
                if (verification.Data.Code == 100)
                {
                    transaction.IsSuccess = true;
                    _userService.UpdateTransaction(transaction);
                    _userService.UpdateWalletBalance(transaction);
                    ViewBag.IsSuccess = true;
                    ViewBag.Code = verification.Data.RefId;
                }
            }

            return View();
        }

        #endregion
    }
}

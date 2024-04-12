using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs.Order;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class MyOrdersController : Controller
    {

        #region DI

        private readonly IOrderService _orderService;

        public MyOrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion


        [Route("user-panel/my-orders")]
        public IActionResult Index()
        {
            return View(_orderService.GetUserOrders(User.Identity.Name));
        }

        [Route("user-panel/my-orders/{orderId}")]
        public IActionResult ShowOrder(int orderId, string type, bool isFinally = false)
        {
            Order order = _orderService.GetOrderForUserPanel(User.Identity.Name, orderId);

            if (order == null)
                return NotFound();

            ViewBag.IsFinally = isFinally;
            ViewBag.Type = type;
            return View(order);
        }

        [Route("user-panel/my-orders/{orderId}/submit")]
        public IActionResult SubmitOrder(int orderId)
        {
            if (_orderService.SubmitOrder(User.Identity.Name, orderId))
            {
                return Redirect($"/user-panel/my-orders/{orderId}?isFinally=true");
            }

            return BadRequest();
        }

        [Route("user-panel/my-orders/apply-discount")]
        public IActionResult ApplyDiscount(int orderId, string discountCode)
        {
            DiscountCodeTypes type = _orderService.ApplyDiscount(orderId, discountCode);

            return Redirect($"/user-panel/my-orders/{orderId}?type={type.ToString()}");
        }
    }
}

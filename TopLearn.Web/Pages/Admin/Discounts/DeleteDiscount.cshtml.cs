using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Discounts
{
    public class DeleteDiscountModel : PageModel
    {

        #region DI

        private readonly IOrderService _orderService;

        public DeleteDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        public void OnGet(int discountId)
        {
            Discount discount = _orderService.GetDiscountById(discountId);

            if (discount != null)
                _orderService.DeleteDiscount(discountId);
        }
    }
}

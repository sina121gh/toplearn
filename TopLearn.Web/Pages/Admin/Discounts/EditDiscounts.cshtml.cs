using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Discounts
{
    public class EditDiscountsModel : PageModel
    {

        #region DI

        private readonly IOrderService _orderService;

        public EditDiscountsModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        [BindProperty]
        public Discount Discount { get; set; }

        public void OnGet(int discountId)
        {
            Discount = _orderService.GetDiscountById(discountId);
        }

        public IActionResult OnPost(string startDate = "", string endDate = "")
        {
            if (startDate != "")
                Discount.StartDate = startDate.ToMiladi();

            if (endDate != "")
                Discount.EndDate = endDate.ToMiladi();

            if (!ModelState.IsValid)
                return Page();

            _orderService.UpdateDiscount(Discount);

            return Redirect("/admin/discounts");
        }
    }
}

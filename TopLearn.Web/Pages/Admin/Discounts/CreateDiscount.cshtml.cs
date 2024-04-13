using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace TopLearn.Web.Pages.Admin.Discounts
{
    public class CreateDiscountModel : PageModel
    {

        #region DI

        private readonly IOrderService _orderService;

        public CreateDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        [BindProperty]
        public Discount Discount { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string startDate = "", string endDate = "")
        {
            if (startDate != "")
                Discount.StartDate = startDate.ToMiladi();

            if (endDate != "")
                Discount.EndDate = endDate.ToMiladi();

            if (!ModelState.IsValid)
                return Page();

            _orderService.AddDiscount(Discount);

            return Redirect("/admin/discounts");
        }
    }
}

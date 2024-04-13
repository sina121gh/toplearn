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
            if (startDate != null)
                Discount.StartDate = startDate.ToMiladi();

            if (endDate != null)
                Discount.EndDate = endDate.ToMiladi();

            if (!ModelState.IsValid || _orderService.DoesCodeExist(Discount.Code))
                return Page();

            _orderService.AddDiscount(Discount);

            return Redirect("/admin/discounts");
        }


        public IActionResult OnGetValidateCode(string code)
        {
            return Content((!_orderService.DoesCodeExist(code)).ToString());
        }
    }
}

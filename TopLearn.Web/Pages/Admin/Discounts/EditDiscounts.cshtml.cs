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
            if (startDate != null)
                Discount.StartDate = startDate.ToMiladi();

            if (endDate != null)
                Discount.EndDate = endDate.ToMiladi();

            if (!ModelState.IsValid)
                return Page();

            string currentCode = _orderService.GetDiscountCodeById(Discount.Id);

            if (currentCode != null && currentCode != Discount.Code)
                if (_orderService.DoesCodeExist(Discount.Code))
                {
                    ViewData["InvalidCode"] = true;
                    return Page();
                }

            _orderService.UpdateDiscount(Discount);

            return Redirect("/admin/discounts");
        }

        public IActionResult OnGetValidateCode(int id ,string code)
        {
            string currentCode = _orderService.GetDiscountCodeById(id);

            if (currentCode != code)
                return Content((!_orderService.DoesCodeExist(code)).ToString());

            return Content("True");
        }
    }
}

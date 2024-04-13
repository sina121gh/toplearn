using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Discounts
{
    public class IndexModel : PageModel
    {
        #region DI

        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        public IEnumerable<Discount> Discounts { get; set; }

        public void OnGet()
        {
            Discounts = _orderService.GetDiscounts();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Order;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(string userName, int courseId);
        bool DoesUserHaveOpenOrder(int userId);
        bool SubmitOrder(string userName, int orderId);
        bool UpdateOrder(Order order);
        Order? GetOrderByUserId(int userId);
        Order GetOrderById(int orderId);
        Order GetOrderForUserPanel(string userName, int orderId);
        OrderDetail? GetOrderDetailByOrderIdAndCourseId(int orderId, int courseId);
        IEnumerable<Order> GetUserOrders(string userName);


        #region Discount

        DiscountCodeTypes ApplyDiscount(int orderId, string discountCode);
        Discount GetDiscountByCode(string discountCode);
        bool UpdateDiscount(Discount discount);

        #endregion
    }
}

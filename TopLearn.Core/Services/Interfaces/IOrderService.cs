using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(string userName, int courseId);
        bool DoesUserHaveOpenOrder(int userId);
        bool SubmitOrder(string userName, int orderId);
        Order? GetOrderByUserId(int userId);
        Order GetOrderById(int orderId);
        Order GetOrderForUserPanel(string userName, int orderId);
        OrderDetail? GetOrderDetailByOrderIdAndCourseId(int orderId, int courseId);
    }
}

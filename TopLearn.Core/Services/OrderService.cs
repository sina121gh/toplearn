using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class OrderService : IOrderService
    {

        private readonly TopLearnContext _context;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;

        public OrderService(TopLearnContext context, IUserService userService, ICourseService courseService)
        {
            _context = context;
            _userService = userService;
            _courseService = courseService;
        }

        public int AddOrder(string userName, int courseId)
        {
            int userId = _userService.GetUserIdByUserName(userName);

            Order order = GetOrderByUserId(userId);
            int coursePrice = _courseService.GetCoursePriceById(courseId).Value;

            if (order == null)
            {
                order = new Order()
                {
                    UserId = userId,
                    IsFinally = false,
                    CreateDate = DateTime.Now,
                    TotalPrice = coursePrice,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            CourseId = courseId,
                            Count = 1,
                            Price = coursePrice,
                        }
                    },
                };

                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                OrderDetail detail = GetOrderDetailByOrderIdAndCourseId(order.Id, courseId);

                if (detail != null)
                {
                    detail.Count++;

                    _context.OrderDetails.Update(detail);
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        CourseId = courseId,
                        Count = 1,
                        Price = coursePrice,
                    };

                    _context.OrderDetails.Add(detail);
                }

                order.TotalPrice += coursePrice;
                _context.Orders.Update(order);
                _context.SaveChanges();
                
            }

            return order.Id;
        }

        public bool DoesUserHaveOpenOrder(int userId)
        {
            return _context.Orders
                .Any(o => o.UserId == userId && !o.IsFinally);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public Order? GetOrderByUserId(int userId)
        {
            return _context.Orders
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinally);
        }

        public OrderDetail? GetOrderDetailByOrderIdAndCourseId(int orderId, int courseId)
        {
            return _context.OrderDetails
                .FirstOrDefault(od => od.OrderId == orderId && od.CourseId == courseId);
        }
    }
}

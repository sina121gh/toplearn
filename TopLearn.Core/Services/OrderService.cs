using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TopLearn.Core.DTOs.Order;
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

        public bool AddDiscount(Discount discount)
        {
            try
            {
                _context.Discounts.Add(discount);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
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

        public DiscountCodeTypes ApplyDiscount(int orderId, string discountCode)
        {
            Discount discount = GetDiscountByCode(discountCode);

            #region Discount Code Validation

            if (discount == null)
                return DiscountCodeTypes.Invalid;

            if (discount.StartDate != null && discount.StartDate > DateTime.Now)
                return DiscountCodeTypes.Expired;

            if (discount.EndDate != null && discount.EndDate < DateTime.Now)
                return DiscountCodeTypes.Expired;

            if (discount.UsableCount != null && discount.UsableCount == 0)
                return DiscountCodeTypes.Finished;

            #endregion

            Order order = GetOrderById(orderId);

            if (HasDiscountUsedByUser(order.UserId, discount.Id))
                return DiscountCodeTypes.Used;

            order.TotalPrice -= ((order.TotalPrice * discount.Precent) / 100);
            UpdateOrder(order);

            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
                UpdateDiscount(discount);
            }

            _context.UserDiscounts.Add(new UserDiscount()
            {
                UserId = order.UserId,
                DiscountId = discount.Id,
            });

            _context.SaveChanges();

            return DiscountCodeTypes.Success;
        }

        public bool DeleteDiscount(int discountId)
        {
            Discount discount = GetDiscountById(discountId);

            if (discount == null)
                return false;

            try
            {
                _context.Discounts.Remove(discount);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DoesUserHaveOpenOrder(int userId)
        {
            return _context.Orders
                .Any(o => o.UserId == userId && !o.IsFinally);
        }

        public Discount GetDiscountByCode(string discountCode)
        {
            return _context.Discounts
                .SingleOrDefault(d => d.Code == discountCode);
        }

        public Discount GetDiscountById(int discountId)
        {
            return _context.Discounts.Find(discountId);
        }

        public List<Discount> GetDiscounts()
        {
            return _context.Discounts.ToList();
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

        public Order GetOrderForUserPanel(string userName, int orderId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Course)
                .SingleOrDefault(o => o.UserId == userId && o.Id == orderId);
        }

        public IEnumerable<Order> GetUserOrders(string userName)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders
                .Where(o => o.UserId == userId)
                .ToList();
        }

        public bool HasDiscountUsedByUser(int userId, int discountId)
        {
            return _context.UserDiscounts
                .Any(ud => ud.UserId == userId && ud.DiscountId == discountId);
        }

        public bool SubmitOrder(string userName, int orderId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            Order order = GetOrderForUserPanel(userName, orderId);

            if (order == null || order.IsFinally)
                return false;

            if (_userService.GetWalletBalance(userName) >= order.TotalPrice)
            {
                order.IsFinally = true;
                int transactionId = _userService.WithdrawWallet(userName, order.TotalPrice,
                    $"پرداخت فاکتور شماره #{orderId}", true);
                _userService.UpdateWalletBalance(transactionId);

                _context.Orders.Update(order);

                foreach (OrderDetail orderDetail in order.OrderDetails)
                {
                    _context.UserCourses.Add(new DataLayer.Entities.Course.UserCourse()
                    {
                        UserId = userId,
                        CourseId = orderDetail.CourseId,
                    });
                }

                return _context.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateDiscount(Discount discount)
        {
            try
            {
                _context.Discounts.Update(discount);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateOrder(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

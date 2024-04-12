using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.DataLayer.Entities.User
{
    public class User
    {

        public User()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? UserName { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? Password { get; set; }

        [Display(Name = "کد فعال سازی")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? ActiveCode { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? Avatar { get; set; }
        
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }

        public bool IsDeleted { get; set; }

        [MaxLength(29)]
        public string? Salt { get; set; }

        #region Relations

        public virtual IEnumerable<UserRole>? UserRoles { get; set; }

        public virtual Wallet.Wallet Wallet { get; set; }

        public virtual IEnumerable<Transaction> Transactions { get; set; }

        public virtual IEnumerable<Course.Course> Courses { get; set; }

        public virtual IEnumerable<Order.Order> Orders { get; set; }

        public IEnumerable<UserCourse>? UserCourses { get; set; }

        #endregion
    }
}

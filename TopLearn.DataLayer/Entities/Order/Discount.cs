using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Order
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = "{0} الزامی است")]
        [MaxLength(150, ErrorMessage = "{0} نمیتواند بیشتر از {1}‌ باشد")]
        public string Code { get; set; }

        [Display(Name = "درصد")]
        [Required(ErrorMessage = "{0} الزامی است")]
        public int Precent { get; set; }

        [Display(Name = "تعداد استفاده")]
        public int? UsableCount { get; set; }


        [Display(Name = "تاریخ شروع")]
        public DateTime? StartDate { get; set; }


        [Display(Name = "تاریخ پایان")]
        public DateTime? EndDate { get; set;}

        #region Relations

        public List<UserDiscount>? UserDiscounts { get; set; }

        #endregion
    }
}

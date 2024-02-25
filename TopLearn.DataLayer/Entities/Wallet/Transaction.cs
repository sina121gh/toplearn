using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Wallet
{
    public class Transaction
    {
        public Transaction()
        {
            
        }


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نوع تراکنش")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کاربر")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "مبلغ")]
        public uint Amount { get; set; }

        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "فیلد {0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? Description { get; set; }

        [Display(Name = "وضعیت پرداخت")]
        public bool IsPaid { get; set; }

        [Display(Name = "تاریخ و ساعت")]
        public DateTime CreateDate { get; set; }


        #region Relations

        [ForeignKey("TypeId")]
        public virtual TransactionType TransactionType { get; set; }

        [ForeignKey("UserId")]
        public virtual User.User User { get; set; }

        #endregion


    }
}

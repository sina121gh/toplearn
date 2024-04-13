﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.DataLayer.Entities.User
{
    public class UserDiscount
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int DiscountId { get; set; }


        #region Relations

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("DiscountId")]
        public Discount? Discount { get; set; }

        #endregion
    }
}

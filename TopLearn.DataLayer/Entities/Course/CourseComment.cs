using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseComment
    {
        [Key]
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int UserId { get; set; }

        [MaxLength(700, ErrorMessage = "{0} نمیتواند بیشتر از کاراکتر {1} باشد")]
        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }

        public int? ParentId { get; set; }

        public bool HasAdminRead { get; set; }



        #region Relations

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; }

        #endregion
    }
}

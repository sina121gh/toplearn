using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Course
{
    public class UserCourse
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        #region Relations

        [ForeignKey("UserId")]
        public User.User? User { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        #endregion
    }
}

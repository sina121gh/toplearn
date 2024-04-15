using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseVote
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int UserId { get; set; }


        [Required]
        public int CourseId { get; set; }


        [Required]
        public bool Vote { get; set; }


        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        #region Relations

        [ForeignKey("UserId")]
        public User.User? User { get; set; }


        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        #endregion
    }
}

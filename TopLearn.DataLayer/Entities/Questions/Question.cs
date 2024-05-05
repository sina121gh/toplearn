using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Questions
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "فیلد {0} الزامی است")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "فیلد {0} الزامی است")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "فیلد {0} الزامی است")]
        [Display(Name = "عنوان سوال")]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "فیلد {0} الزامی است")]
        [Display(Name = "متن سوال")]
        public string Body { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }

        #region Relations

        [ForeignKey("CourseId")]
        public Course.Course? Course { get; set; }

        [ForeignKey("UserId")]
        public User.User? User { get; set; }

        public IEnumerable<Answer>? Answers { get; set; }

        #endregion
    }
}

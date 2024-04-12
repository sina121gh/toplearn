using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Course
{
    public class Course
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "لطفا {0}‌ را وارد کنید")]
        public int GroupId { get; set; }


        public int? SubGroupId { get; set; }


        [Required(ErrorMessage = "لطفا {0}‌ را وارد کنید")]
        public int TeacherId { get; set; }


        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0}‌ را وارد کنید")]
        public int StatusId { get; set; }


        [Display(Name = "سطح دوره")]
        [Required(ErrorMessage = "لطفا {0}‌ را وارد کنید")]
        public int LevelId { get; set; }


        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0}‌ را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }


        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0}‌ را وارد کنید")]
        public string Description { get; set; }


        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0}‌ را وارد کنید")]
        public int Price { get; set; }


        [MaxLength(600, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? Tags { get; set; }


        [MaxLength(50)]
        public string? ImageName { get; set; }


        [MaxLength(100)]
        public string? DemoFileName { get; set; }


        [Required]
        public DateTime CreateDate { get; set; }


        public DateTime? UpdateDate { get; set; }


        #region Relations

        [ForeignKey("TeacherId")]
        public User.User? User { get; set; }


        [ForeignKey("GroupId")]
        public CourseGroup? Group { get; set; }


        [ForeignKey("SubGroupId")]
        public CourseGroup? SubGroup { get; set; }


        [ForeignKey("StatusId")]
        public CourseStatus? CourseStatus { get; set; }


        [ForeignKey("LevelId")]
        public CourseLevel? CourseLevel { get; set; }


        public List<CourseEpisode>? CourseEpisodes { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }

        public List<UserCourse>? UserCourses { get; set; }

        #endregion
    }
}

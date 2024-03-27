using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseEpisode
    {
        [Key]
        public int Id { get; set; }


        public int CourseId { get; set; }


        [Display(Name = "عنوان بخش")]
        [Required(ErrorMessage = "لطفا {0}‌ را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }



        [Display(Name = "زمان بخش")]
        [Required(ErrorMessage = "لطفا {0}‌ را وارد کنید")]
        public TimeSpan Time { get; set; }



        [Display(Name = "نام فایل")]
        [MaxLength(100)]
        public string? FileName { get; set; }


        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }


        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Course
{
    public class CourseLevel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "")]
        [MaxLength(50)]
        public string Title { get; set; }


        public List<Course> Courses { get; set; }
    }
}

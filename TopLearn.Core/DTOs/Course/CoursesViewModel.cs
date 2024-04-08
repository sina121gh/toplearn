using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.Course
{
    public class CoursesListForAdminViewModel
    {
        public List<CourseForAdminViewModel> Courses { get; set; }
        public int Take { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }

    public class CourseForAdminViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int EpisodeCount { get; set; }
    }

    public class ShowCoursesListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        public TimeSpan TotalTime { get; set; }
    }
}

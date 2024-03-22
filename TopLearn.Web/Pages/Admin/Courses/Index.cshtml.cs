using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.Course;

namespace TopLearn.Web.Pages.Courses
{
    [PermissionChecker("مدیریت دوره ها")]
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public CoursesForAdminViewModel CoursesViewModel { get; set; }

        public void OnGet()
        {
            CoursesViewModel = _courseService.GetCoursesForAdmin();
        }
    }
}

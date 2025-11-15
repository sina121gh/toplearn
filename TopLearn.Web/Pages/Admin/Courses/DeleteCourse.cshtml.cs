using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class DeleteCourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public DeleteCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public void OnGet(int courseId)
        {
            _courseService.DeleteCourse(courseId);
        }
    }
}

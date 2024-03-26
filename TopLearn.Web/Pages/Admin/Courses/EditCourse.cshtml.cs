using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Courses
{
    public class EditCourseModel : PageModel
    {
        #region DI

        private readonly ICourseService _courseService;

        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion


        [BindProperty]
        public Course Course { get; set; }

        public void OnGet(int courseId)
        {
            Course = _courseService.GetCourseById(courseId);
        }

        public IActionResult OnPost(IFormFile? courseImageInput, IFormFile? demoFile)
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.UpdateCourse(Course, courseImageInput, demoFile);

            return Redirect("/admin/courses/");
        }
    }
}

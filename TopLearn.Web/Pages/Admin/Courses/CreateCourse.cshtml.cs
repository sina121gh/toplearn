using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TopLearn.Web.Pages.Admin.Courses
{
    [PermissionChecker("افزودن دوره")]
    public class CreateCourseModel : PageModel
    {
        #region DI

        private readonly ICourseService _courseService;


        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public Course Course { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(IFormFile? courseImageInput, IFormFile? demoFile)
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.AddCourse(Course, courseImageInput, demoFile);

            return Redirect("/admin/courses/");
        }
    }
}

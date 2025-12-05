using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace TopLearn.Web.Pages.Admin.Courses
{
    [IgnoreAntiforgeryToken]
    public class DeleteCourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public DeleteCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> OnDelete(int courseId)
        {
            bool success = await _courseService.DeleteCourseAsync(courseId);
            return new JsonResult(new { success = success });
        }
    }
}

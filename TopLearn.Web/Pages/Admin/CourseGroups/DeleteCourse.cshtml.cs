using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.CourseGroups
{
    [IgnoreAntiforgeryToken]
    public class DeleteCourseModel : PageModel
    {

        #region DI

        private readonly ICourseService _courseService;

        public DeleteCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        public IActionResult OnDelete(int groupId)
        {
            return Content(_courseService.DeleteGroup(groupId).ToString());
        }
    }
}

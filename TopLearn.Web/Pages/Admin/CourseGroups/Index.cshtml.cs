using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.CorseGroups
{
    public class IndexModel : PageModel
    {
        #region DI

        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        public IEnumerable<CourseGroup> CourseGroups { get; set; }

        public void OnGet()
        {
            CourseGroups = _courseService.GetAllGroupsIncludingSubGroups();
        }
    }
}

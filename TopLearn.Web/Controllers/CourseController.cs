using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Controllers
{
    public class CourseController : Controller
    {

        #region DI

        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        public IActionResult Index(int pageId = 1, string filter = "", string getType = "all",
            string orderBy = "createDate", int minPrice = 0, int maxPrice = 0,
            List<int> selectedGroups = null)
        {
            return View(_courseService.GetCourses(pageId, 9, filter, getType, orderBy,
                minPrice, maxPrice, selectedGroups));
        }
    }
}

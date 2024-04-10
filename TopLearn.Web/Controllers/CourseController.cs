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

        [Route("/courses")]
        public IActionResult Index(int pageId = 1, string filter = "", string getType = "all",
            string orderBy = "createDate", int minPrice = 0, int maxPrice = 0,
            List<int> selectedGroups = null)
        {
            ViewData["PageId"] = pageId;
            ViewData["Groups"] = _courseService.GetGroups();
            ViewData["SelectedGroups"] = selectedGroups;
            ViewData["GetType"] = getType;
            return View(_courseService.GetCourses(pageId, 1, filter, getType, orderBy,
                minPrice, maxPrice, selectedGroups));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Controllers
{
    public class CourseController : Controller
    {

        #region DI

        private readonly ICourseService _courseService;
        private readonly IOrderService _orderService;

        public CourseController(ICourseService courseService, IOrderService orderService)
        {
            _courseService = courseService;
            _orderService = orderService;
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

        [Route("courses/{courseId}")]
        public IActionResult ShowCourse(int courseId)
        {
            Course course = _courseService.GetCourseForShowDetails(courseId);

            if (course == null) 
                return NotFound();

            return View(course);
        }

        [Authorize]
        [Route("/courses/{courseId}/buy")]
        public IActionResult BuyCourse(int courseId)
        {
            _orderService.AddOrder(User.Identity.Name, courseId);
            return Redirect($"/courses/{courseId}/");
        }
    }
}

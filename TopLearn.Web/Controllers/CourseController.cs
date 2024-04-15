using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Controllers
{
    public class CourseController : Controller
    {

        #region DI

        private readonly ICourseService _courseService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public CourseController(ICourseService courseService,
            IOrderService orderService,
            IUserService userService)
        {
            _courseService = courseService;
            _orderService = orderService;
            _userService = userService;
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
            int orderId = _orderService.AddOrder(User.Identity.Name, courseId);
            return Redirect($"/user-panel/my-orders/{orderId}/");
        }

        [Route("download/{episodeId}")]
        public IActionResult DownloadFile(int episodeId)
        {
            CourseEpisode episode = _courseService.GetEpisodeById(episodeId);

            string fileName = episode.FileName;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "courses",
                "episodes",
                fileName);


            if (episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", fileName);
            }

            if (User.Identity.IsAuthenticated)
            {
                if (_orderService.IsUserInCourse(User.Identity.Name, episode.CourseId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filePath);
                    return File(file, "application/force-download", fileName);
                }
            }

            return Forbid();
        }

        //[Route("/courses/{courseId}/create-comment")]
        [HttpPost]
        public IActionResult CreateComment(CourseComment comment)
        {
            comment.CreateDate = DateTime.Now;
            comment.HasAdminRead = false;
            comment.UserId = _userService.GetUserIdByUserName(User.Identity.Name);
            _courseService.AddComment(comment);

            return View("ShowCourseComments", _courseService.GetCourseComments(comment.CourseId));
        }

        [Route("courses/{courseId}/comments")]
        public IActionResult ShowCourseComments(int courseId, int pageId = 1)
        {
            return View(_courseService.GetCourseComments(courseId, pageId));
        }

        [Route("courses/{courseId}/votes")]
        public IActionResult CourseVotes(int courseId)
        {
            if (!_courseService.IsCourseFree(courseId))
            {
                if (!_orderService.IsUserInCourse(User.Identity.Name, courseId))
                    ViewData["AccessDenied"] = false;
            }
            return PartialView(_courseService.GetCourseVotes(courseId));
        }


        [Authorize]
        [Route("courses/{courseId}/votes/create")]
        public IActionResult AddVote(int courseId, bool vote)
        {
            _courseService.AddVote(User.Identity.Name, courseId, vote);
            return PartialView("CourseVotes", _courseService.GetCourseVotes(courseId));
        }

        [Route("get-groups")]
        public IActionResult ShowGroups()
        {
            return View("/Pages/Admin/CourseGroups/GroupsListView.cshtml", _courseService.GetAllGroupsIncludingSubGroups());
        }
    }
}

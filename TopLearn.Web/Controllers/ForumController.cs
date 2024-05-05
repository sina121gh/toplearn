using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopLearn.DataLayer.Entities.Questions;

namespace TopLearn.Web.Controllers
{
    public class ForumController : Controller
    {

        #region DI

        private readonly IForumService _forumService;
        private readonly ICourseService _courseService;
        private readonly IOrderService _orderService;

        public ForumController(IForumService forumService,
            ICourseService courseService,
            IOrderService orderService)
        {
            _forumService = forumService ?? throw new ArgumentNullException(nameof(forumService));
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("courses/{courseId}/questions/create")]
        public IActionResult CreateQuestion(int courseId)
        {
            Question question = new Question()
            {
                CourseId = courseId,
            };
            return View(question);
        }

        [HttpPost]
        [Authorize]
        [Route("courses/{courseId}/questions/create")]
        public IActionResult CreateQuestion(Question question)
        {
            if (!ModelState.IsValid)
                return View(question);

            Course course = _courseService.GetCourseById(question.CourseId);

            if (course.Price != 0)
            {
                if (!_orderService.IsUserInCourse(User.Identity.Name, question.CourseId))
                {
                    return BadRequest();
                }
            }

            question.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            int questionId = _forumService.AddQuestion(question);

            return Redirect($"/courses/{question.CourseId}/questions");
        }
    }
}

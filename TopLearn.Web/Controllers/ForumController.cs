using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopLearn.DataLayer.Entities.Course;
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

        [Route("courses/{courseId}/questions")]
        public IActionResult Index(int courseId, int pageId = 1, int take = 5, string filter = "")
        {
            //if (!_courseService.IsCourseFree(courseId))
            //{
            //    if (User.Identity.IsAuthenticated && 
            //        (!_orderService.IsUserInCourse(User.Identity.Name, courseId)))
            //    {
            //            return Forbid();
            //    }
            //    else
            //    {

            //            return Forbid();
            //    }
            //}

            ViewBag.PageId = pageId;
            ViewBag.Filter = filter;
            return View(_forumService.GetQuestionsByCourseIdWithIncludes(courseId, filter, pageId, take));
        }

        #region Create Question

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

            return Redirect($"/courses/{question.CourseId}/questions/{questionId}");
        }

        #endregion

        #region Show Question

        [Route("courses/{courseId}/questions/{questionId}")]
        public IActionResult ShowQuestion(int questionId)
        {
            return View(_forumService.ShowQuestion(questionId));
        }

        #endregion

        #region Answer

        [Route("courses/{courseId}/questions/{questionId}/answers-create")]
        [Authorize]
        [HttpPost]
        public IActionResult Answer(int courseId, int questionId, string body)
        {
            if (!string.IsNullOrEmpty(body))
            {
                Question? question = _forumService.GetQuestionById(questionId);

                if (question == null)
                    return NotFound();

                var sanitizer = new HtmlSanitizer();
                body = sanitizer.Sanitize(body);

                if (question.CourseId != courseId)
                    return BadRequest();
                _forumService.AddAnswer(new Answer()
                {
                    Body = body,
                    CreateDate = DateTime.Now,
                    QuestionId = questionId,
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                });
            }
            return Redirect($"/courses/{courseId}/questions/{questionId}");
        }

        [HttpGet]
        [Authorize]
        [Route("/questions/{questionId}/answers/{answerId}")]
        public IActionResult GetAnswerBodyForEdit(int questionId, int answerId)
        {
            Answer? answer = _forumService.GetAnswerById(questionId, answerId);
            if (answer == null)
                return NotFound();

            if (answer.UserId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                return Forbid();

            return Ok(answer);
        }

        [Authorize]
        public IActionResult SelectTrueAnswer(int questionId, int answerId)
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Question question = _forumService.GetQuestionById(questionId);

            if (question.UserId != currentUserId)
                return BadRequest();

            _forumService.ChangeTrueAnswer(questionId, answerId);
            return Redirect($"/courses/{question.CourseId}/questions/{questionId}");
        }

        #endregion
    }
}

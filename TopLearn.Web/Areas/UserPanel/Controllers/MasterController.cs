using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    [PermissionChecker("پنل مدرس")]
    public class MasterController : Controller
    {
        #region Ctor

        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public MasterController(ICourseService courseService, IUserService userService, IFileService fileService)
        {

            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        #endregion

        [HttpGet("master-courses")]
        public IActionResult MasterCoursesList()
        {
            return View(_courseService.GetAllMasterCourses(User.Identity.Name));
        }


        [HttpGet("master-courses/{courseId}/episodes")]
        public IActionResult EpisodesList(int courseId)
        {
            if (!_courseService.DoesCourseExist(courseId))
                return NotFound();

            if (_courseService.GetCourseTeacherId(courseId) != _userService.GetUserIdByUserName(User.Identity.Name))
                return Redirect("master-courses");

            ViewBag.CourseId = courseId;
            return View(_courseService.GetCourseEpisodes(courseId));
        }


        [HttpGet("master-courses/{courseId}/episodes/add")]
        public IActionResult AddEpisode(int courseId)
        {
            if (!_courseService.DoesCourseExist(courseId))
                return NotFound();

            if (_courseService.GetCourseTeacherId(courseId) != _userService.GetUserIdByUserName(User.Identity.Name))
                return Redirect("master-courses");

            return View();
        }


        [HttpPost("master-courses/{courseId}/episodes/add")]
        public IActionResult AddEpisode(AddEpisodeViewModel episodeViewModel)
        {
            if (!ModelState.IsValid)
                return View(episodeViewModel);

            if (string.IsNullOrEmpty(episodeViewModel.FileName))
                return View(episodeViewModel);

            bool result = _courseService.AddEpisode(episodeViewModel, User.Identity.Name);

            if (result)
                return Redirect($"/master-courses/{episodeViewModel.CourseId}/episodes");

            return View();
        }


        [HttpPost("master-courses/{courseId}/episodes/add/dropzone")]
        public IActionResult DropzoneTarget(List<IFormFile> files, int courseId)
        {
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    var fileName = $"{courseId}-{Guid.NewGuid().ToString().Replace("-", "")}" + Path.GetExtension(file.FileName);

                    string uploadedFileName = _fileService.SaveEpisodeFile(file, fileName);

                    return Ok(new { data = uploadedFileName, status = "Success" });
                }
                
            }

            return new JsonResult(new { status = "Error" });
        }
    }
}

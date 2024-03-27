using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Courses.Episodes
{
    public class CreateEpisodeModel : PageModel
    {

        #region DI

        private readonly ICourseService _courseService;

        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public CourseEpisode Episode { get; set; }

        public void OnGet(int courseId)
        {
            Episode = new CourseEpisode();
            Episode.CourseId = courseId;
        }

        public IActionResult OnPost(IFormFile episodeFile)
        {
            if (!ModelState.IsValid || episodeFile == null)
                return Page();

            if (_courseService.DoesEpisodeExist(episodeFile.FileName))
            {
                ViewData["DoesFileExist"] = true;
                return Page();
            }

            _courseService.AddEpisode(Episode, episodeFile);

            return Redirect($"/admin/courses/{Episode.CourseId}/episodes");
        }
    }
}

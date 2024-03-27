using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Courses.Episodes
{
    public class EditEpisodeModel : PageModel
    {

        #region DI

        private readonly ICourseService _courseService;

        public EditEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public CourseEpisode Episode { get; set; }

        public void OnGet(int episodeId)
        {
            Episode = _courseService.GetEpisodeById(episodeId);
        }

        public IActionResult OnPost(IFormFile? episodeFile)
        {
            if (!ModelState.IsValid)
                return Page();

            if (episodeFile != null)
            {
                if (_courseService.DoesEpisodeExist(episodeFile.FileName))
                {
                    ViewData["DoesFileExist"] = true;
                    return Page();
                }
            }

            _courseService.UpdateEpisode(Episode, episodeFile);

            return Redirect($"/admin/courses/{Episode.CourseId}/episodes");
        }
    }
}

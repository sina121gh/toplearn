using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Courses.Episodes
{
    [IgnoreAntiforgeryToken]
    public class DeleteEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;


        public DeleteEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult OnDelete(int episodeId)
        {
            var success = _courseService.DeleteEpisode(episodeId);
            return new JsonResult(new { success = success });
        }
    }
}

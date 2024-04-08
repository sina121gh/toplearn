using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Courses.Episodes
{
    public class DeleteEpisodeModel : PageModel
    {
        private readonly ICourseService _courseService;


        public DeleteEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public void OnGet(int episodeId)
        {
            _courseService.DeleteEpisode(episodeId);
        }
    }
}

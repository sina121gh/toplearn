using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.Courses.Episodes
{
    public class CourseEpisodesModel : PageModel
    {

        #region DI

        private readonly ICourseService _courseService;

        public CourseEpisodesModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public IEnumerable<CourseEpisode> CourseEpisodes { get; set; }
        public void OnGet(int courseId)
        {
            CourseId = courseId;
            CourseTitle = _courseService.GetCourseTitleById(courseId);
            CourseEpisodes = _courseService.GetCourseEpisodes(courseId);
        }
    }
}

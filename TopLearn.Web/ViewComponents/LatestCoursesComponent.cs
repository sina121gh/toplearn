using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.ViewComponents
{
    public class LatestCoursesComponent : ViewComponent
    {
        #region DI

        private readonly ICourseService _courseService;

        public LatestCoursesComponent(ICourseService courseService)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult) View("LatestCourses", _courseService.GetCourses()));
        }
    }
}

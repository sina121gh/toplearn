using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.CorseGroups
{
    public class CreateGroupModel : PageModel
    {
        #region DI

        private readonly ICourseService _courseService;

        public CreateGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public CourseGroup Group { get; set; }

        public void OnGet(int? parentId)
        {
            Group = new CourseGroup()
            {
                ParentId = parentId,
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (_courseService.AddGroup(Group))
                return Redirect("/admin/course-groups");

            return BadRequest();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Pages.Admin.CourseGroups
{
    public class EditGroupModel : PageModel
    {

        #region DI

        private readonly ICourseService _courseService;

        public EditGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #endregion

        [BindProperty]
        public CourseGroup Group { get; set; }

        public void OnGet(int groupId)
        {
            Group = _courseService.GetGroupById(groupId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (_courseService.UpdateGroup(Group))
                return Redirect("/admin/course-groups");

            return BadRequest();
        }
    }
}

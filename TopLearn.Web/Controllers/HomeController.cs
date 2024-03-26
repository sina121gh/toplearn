using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TopLearn.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPermisionService _permisionService;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;

        public HomeController(IPermisionService permisionService,
            IUserService userService,
            ICourseService courseService)
        {
            _permisionService = permisionService;
            _userService = userService;
            _courseService = courseService;

        }

        public IActionResult Index() => View();

        [Route("/error")]
        public IActionResult Error() => View();


        [Authorize]
        public IActionResult Test() => View();


        [Route("/get-roles")]
        public JsonResult GetRoles()
        {
            var roles = _permisionService.GetRoles();
            return Json(roles);
        }

        [Route("/create-role")]
        public JsonResult CreateRole(Role role)
        {
            if (!ModelState.IsValid)
                return Json("Model validation failed", ModelState);

            _permisionService.AddRole(role);

            return Json("Role successfully added");
        }

        [Route("/get-users")]
        public JsonResult GetUsers()
        {
            return Json(_userService.GetUsers().Users.Select(u => new
            {
                Id = u.Id,
                UserName = u.UserName,
                ActiveCode = u.ActiveCode,
                Avatar = u.Avatar,
                Email = u.Email,
                RegisterDate = u.RegisterDate.ToShamsi(),
                IsActive = u.IsActive,
                IsDeleted = u.IsDeleted,
                Password = u.Password,
                Salt = u.Salt,
            }));
        }

        [Route("/get-sub-groups/{groupId}/{selectedGroupId?}")]
        public JsonResult GetSubGroups(int groupId, int selectedGroupId = 0)
        {
            //List<SelectListItem> subGroups = new List<SelectListItem>()
            //{
            //    new SelectListItem(){ Text = "انتخاب کنید", Value = "0", Selected = true, Disabled = true, },
            //};
            //subGroups.AddRange(_courseService.GetSubGroupsForManageCourse(groupId));
            return Json(_courseService.GetSubGroupsForManageCourse(groupId, selectedGroupId));
        }


        [HttpPost]
        [Route("/file-upload")]
        public JsonResult UploadFile(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = MyGenerator.GenerateCode() + Path.GetExtension(upload.FileName).ToLower();

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/uploadedImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }

            var url = $"{"/uploadedImages/"}{fileName}";


            return Json(new { uploaded = true, url });
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Course;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Group

        IEnumerable<CourseGroup> GetGroups();
        IEnumerable<SelectListItem> GetMainGroupsForManageCourse();
        IEnumerable<SelectListItem> GetSubGroupsForManageCourse(int groupId);
        IEnumerable<SelectListItem> GetTeachers();
        IEnumerable<SelectListItem> GetLevels();
        IEnumerable<SelectListItem> GetStatuses();
        IEnumerable<CourseGroup> GetSubGroups(int groupId);
        bool HasSubGroup(int groupId);

        #endregion

        #region Course

        CoursesForAdminViewModel GetCoursesForAdmin(int take = 10, int pageId = 1);
        int AddCourse(Course course, IFormFile courseImg, IFormFile courseDemo);

        #endregion
    }
}

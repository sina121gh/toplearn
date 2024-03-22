using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}

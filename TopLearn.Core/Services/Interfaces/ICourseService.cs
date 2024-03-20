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
        IEnumerable<CourseGroup> GetSubGroups(int groupId);
        bool HasSubGroup(int groupId);

        #endregion
    }
}

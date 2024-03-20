using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly TopLearnContext _context;

        public CourseService(TopLearnContext context)
        {
            _context = context;
        }


        public IEnumerable<CourseGroup> GetGroups()
        {
            return _context.CourseGroups.ToList();
        }

        public IEnumerable<CourseGroup> GetSubGroups(int groupId)
        {
            return _context.CourseGroups
                .Where(g => g.ParentId == groupId)
                .ToList();
        }

        public bool HasSubGroup(int groupId)
        {
            return _context.CourseGroups
                .Any(g => g.ParentId == groupId);
        }
    }
}

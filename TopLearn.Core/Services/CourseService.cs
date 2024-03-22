using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public IEnumerable<SelectListItem> GetLevels()
        {
            return _context.CourseLevels
                .Select(l => new SelectListItem()
                {
                    Value = l.Id.ToString(),
                    Text = l.Title
                }).ToList();
        }

        public IEnumerable<SelectListItem> GetMainGroupsForManageCourse()
        {
            return _context.CourseGroups
                .Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.Title,
                    Value = g.Id.ToString(),
                }).ToList();
        }

        public IEnumerable<SelectListItem> GetStatuses()
        {
            return _context.CourseStatuses
                .Select(s => new SelectListItem()
                {
                    Text = s.Title,
                    Value = s.Id.ToString(),
                }).ToList();
        }

        public IEnumerable<CourseGroup> GetSubGroups(int groupId)
        {
            return _context.CourseGroups
                .Where(g => g.ParentId == groupId)
                .ToList();
        }

        public IEnumerable<SelectListItem> GetSubGroupsForManageCourse(int groupId)
        {
            return _context.CourseGroups
                .Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.Title,
                    Value = g.Id.ToString(),
                }).ToList();
        }

        public IEnumerable<SelectListItem> GetTeachers()
        {
            return _context.UserRoles
                .Where(ur => ur.RoleId == 2)
                .Include(ur => ur.User)
                .Select(ur => new SelectListItem()
                {
                    Text = ur.User.UserName,
                    Value = ur.User.Id.ToString(),
                })
                .ToList(); 
        }

        public bool HasSubGroup(int groupId)
        {
            return _context.CourseGroups
                .Any(g => g.ParentId == groupId);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Course;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly TopLearnContext _context;
        private readonly IFileService _fileService;

        public CourseService(TopLearnContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;

        }

        public int AddCourse(Course course, IFormFile courseImg, IFormFile courseDemo)
        {
            course.CreateDate = DateTime.Now;
            course.ImageName = "DefaultCourseImage.png";

            if (course.SubGroupId == 0)
                course.SubGroupId = null;

            //Todo Check Image
            if (courseImg != null && courseImg.IsImage())
            {
                course.ImageName = _fileService.SaveImage(courseImg);

                Convertors.ImageConvertor.Image_resize(
                Path.Combine(
                Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "courses",
                    "images",
                    course.ImageName),
                Path.Combine(
                Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "courses",
                    "thumbnails",
                    course.ImageName),
                150);
            }

            if (courseDemo != null)
                course.DemoFileName = _fileService.SaveDemo(courseDemo);

            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return course.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public CoursesForAdminViewModel GetCoursesForAdmin(int take = 10, int pageId = 1)
        {
            IQueryable<CourseViewModel> result = _context.Courses
                .Select(c => new CourseViewModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    ImageName = c.ImageName,
                    EpisodeCount = c.CourseEpisodes.Count(),
                });

            int skip = (pageId - 1) * take;

            CoursesForAdminViewModel viewModel = new CoursesForAdminViewModel()
            {
                Courses = result.OrderBy(c => c.Id).Skip(skip).Take(take).ToList(),
                PageCount = (int)Math.Ceiling(result.Count() / (double)take),
                CurrentPage = pageId,
            };

            viewModel.HasNextPage = viewModel.CurrentPage < viewModel.PageCount;
            viewModel.HasPreviousPage = viewModel.CurrentPage > 1;

            return viewModel;
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

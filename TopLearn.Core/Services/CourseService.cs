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

            // Check Image
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

        public int AddEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            episode.FileName = episodeFile.FileName;
            _fileService.SaveEpisodeFile(episodeFile);

            try
            {
                _context.CourseEpisodes.Add(episode);
                _context.SaveChanges();
                return episode.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool DeleteEpisode(int episodeId)
        {
            CourseEpisode episode = GetEpisodeById(episodeId);

            if (episode != null)
            {
                try
                {
                    _context.CourseEpisodes.Remove(episode);
                    _fileService.DeleteEpisode(episode.FileName);
                    return _context.SaveChanges() > 0;
                }
                catch (Exception)
                {
                    return false;
                }
                
            }

            return false;
            
        }

        public bool DoesEpisodeExist(string fileName)
        {
            string episodePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "courses",
                    "episodes",
                    fileName);

            return File.Exists(episodePath);
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public IEnumerable<CourseEpisode> GetCourseEpisodes(int courseId)
        {
            return _context.CourseEpisodes
                .Where(e => e.CourseId == courseId)
                .ToList();
        }

        public IEnumerable<ShowCoursesListViewModel> GetCourses(int pageId = 1, int take = 0, string filter = "",
            string getType = "all", string orderBy = "createDate",
            int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null)
        {
            if (take == 0)
                take = 8;

            IQueryable<Course> result = _context.Courses;

            if (!string.IsNullOrEmpty(filter))
                result = result.Where(c => c.Title.Contains(filter));

            switch (getType)
            {
                case "all":
                    break;

                case "buyable":
                    result = result.Where(c => c.Price > 0);
                    break;
                case "free":
                    result = result.Where(c => c.Price == 0);
                    break;
            }

            switch (orderBy)
            {
                case "createDate":
                    result = result.OrderByDescending(c => c.CreateDate);
                    break;

                case "updateDate":
                    result = result.OrderByDescending(c => c.UpdateDate);
                    break;
            }

            if (minPrice > 0)
                result = result.Where(c => c.Price >= minPrice);

            if (maxPrice > 0)
                result = result.Where(c => c.Price <= maxPrice);

            if (selectedGroups != null && selectedGroups.Any())
            {
                foreach (int groupId in selectedGroups)
                {
                    result = result.Where(c => c.GroupId == groupId || c.SubGroupId == groupId);
                }
            }

            int skip = (pageId - 1) * take;
            return result.Include(c => c.CourseEpisodes).Skip(skip)
            .Take(take).ToList()
                .Select(c => new ShowCoursesListViewModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Price = c.Price,
                    ImageName = c.ImageName,
                    TotalTime = new TimeSpan(c.CourseEpisodes.Sum(e => e.Time.Ticks)),
                });
        }

        public CoursesListForAdminViewModel GetCoursesForAdmin(int take = 10, int pageId = 1)
        {
            IQueryable<CourseForAdminViewModel> result = _context.Courses
                .Select(c => new CourseForAdminViewModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    ImageName = c.ImageName,
                    EpisodeCount = c.CourseEpisodes.Count(),
                });

            int skip = (pageId - 1) * take;

            CoursesListForAdminViewModel viewModel = new CoursesListForAdminViewModel()
            {
                Courses = result.OrderBy(c => c.Id).Skip(skip).Take(take).ToList(),
                PageCount = (int)Math.Ceiling(result.Count() / (double)take),
                CurrentPage = pageId,
            };

            viewModel.HasNextPage = viewModel.CurrentPage < viewModel.PageCount;
            viewModel.HasPreviousPage = viewModel.CurrentPage > 1;

            return viewModel;
        }

        public string GetCourseTitleById(int courseId)
        {
            return _context.Courses
                .Find(courseId)
                .Title;
        }

        public CourseEpisode GetEpisodeById(int episodeId)
        {
            return _context.CourseEpisodes.Find(episodeId);
        }

        public IEnumerable<CourseGroup> GetGroups()
        {
            return _context.CourseGroups.ToList();
        }

        public IEnumerable<SelectListItem> GetLevels(int selectedLevelId = 0)
        {
            return _context.CourseLevels
                .Select(l => new SelectListItem()
                {
                    Value = l.Id.ToString(),
                    Text = l.Title,
                    Selected = selectedLevelId == l.Id,
                }).ToList();
        }

        public IEnumerable<CourseGroup> GetMainGroups()
        {
            return _context.CourseGroups.Where(g => g.ParentId == null).ToList();
        }

        public IEnumerable<SelectListItem> GetMainGroupsForManageCourse(int selectedGroupId = 0)
        {

            return _context.CourseGroups
                .Where(g => g.ParentId == null)
                .Select(g => new SelectListItem()
                {
                    Text = g.Title,
                    Value = g.Id.ToString(),
                    Selected = selectedGroupId == g.Id
                }).ToList();
        }

        public IEnumerable<SelectListItem> GetStatuses(int selectedStatusId = 0)
        {
            return _context.CourseStatuses
                .Select(s => new SelectListItem()
                {
                    Text = s.Title,
                    Value = s.Id.ToString(),
                    Selected = selectedStatusId == s.Id,
                }).ToList();
        }

        public IEnumerable<CourseGroup> GetSubGroups(int groupId)
        {
            return _context.CourseGroups
                .Where(g => g.ParentId == groupId)
                .ToList();
        }

        public IEnumerable<SelectListItem> GetSubGroupsForManageCourse(int groupId, int selectedGroupId = 0)
        {
            return _context.CourseGroups
                .Where(g => g.ParentId == groupId)
                .Select(g => new SelectListItem()
                {
                    Text = g.Title,
                    Value = g.Id.ToString(),
                    Selected = selectedGroupId == g.Id
                }).ToList();
        }

        public IEnumerable<SelectListItem> GetTeachers(int selectedTeacherId)
        {
            return _context.UserRoles
                .Where(ur => ur.RoleId == 2)
                .Include(ur => ur.User)
                .Select(ur => new SelectListItem()
                {
                    Text = ur.User.UserName,
                    Value = ur.User.Id.ToString(),
                    Selected = selectedTeacherId == ur.Id,
                })
                .ToList();
        }

        public bool HasSubGroup(int groupId)
        {
            return _context.CourseGroups
                .Any(g => g.ParentId == groupId);
        }

        public bool UpdateCourse(Course course, IFormFile courseImg, IFormFile courseDemo)
        {
            course.UpdateDate = DateTime.Now;

            if (course.SubGroupId == 0)
                course.SubGroupId = null;

            // Check Image
            if (courseImg != null && courseImg.IsImage())
            {
                // Deletes Previous Images
                _fileService.DeleteImage(course.ImageName);
                _fileService.DeleteThumbnail(course.ImageName);

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
            {
                _fileService.DeleteDemo(course.DemoFileName);
                course.DemoFileName = _fileService.SaveDemo(courseDemo);
            }
                
            try
            {
                _context.Courses.Update(course);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateEpisode(CourseEpisode episode, IFormFile episodeFile)
        {
            if (episodeFile != null)
            {
                _fileService.DeleteEpisode(episode.FileName);
                episode.FileName = _fileService.SaveEpisodeFile(episodeFile);
            }

            try
            {
                _context.CourseEpisodes.Update(episode);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

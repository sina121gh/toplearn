﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Course;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Core.Services.Interfaces
{
    public interface ICourseService
    {
        #region Group

        IEnumerable<CourseGroup> GetAllGroupsIncludingSubGroups();
        IEnumerable<CourseGroup> GetGroups();
        IEnumerable<CourseGroup> GetMainGroups();
        IEnumerable<SelectListItem> GetMainGroupsForManageCourse(int selectedGroupId = 0);
        IEnumerable<SelectListItem> GetSubGroupsForManageCourse(int groupId, int selectedGroupId = 0);
        IEnumerable<SelectListItem> GetTeachers(int selectedTeacherId = 0);
        IEnumerable<SelectListItem> GetLevels(int selectedLevelId = 0);
        IEnumerable<SelectListItem> GetStatuses(int selectedStatusId = 0);
        IEnumerable<CourseGroup> GetSubGroups(int groupId);
        CourseGroup GetGroupById(int groupId);
        bool HasSubGroup(int groupId);
        bool AddGroup(CourseGroup group);
        bool UpdateGroup(CourseGroup group);
        bool DeleteGroup(int groupId);
        bool DeleteSubGroupsByGroupId(int groupId);

        #endregion

        #region Course

        IEnumerable<ShowCourseItemViewModel> GetPopularCourses();

        IEnumerable<Course> GetAllMasterCourses(string userName);

        Course GetCourseForShowDetails(int courseId);

        ShowCoursesListViewModel GetCourses(int pageId = 1, int take = 0, string filter = "", string getType = "all",
            string orderBy = "createDate", int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null);

        CoursesListForAdminViewModel GetCoursesForAdmin(int take = 10, int pageId = 1);

        int? GetCoursePriceById(int courseId);

        int AddCourse(Course course, IFormFile courseImg, IFormFile courseDemo);

        int GetCourseTeacherId(int courseId);

        string GetCourseTitleById(int courseId);

        Course GetCourseById(int courseId);

        bool UpdateCourse(Course course, IFormFile courseImg, IFormFile courseDemo);

        bool IsCourseFree(int courseId);

        bool DoesCourseExist(int courseId);


        #endregion

        #region Episode

        IEnumerable<CourseEpisode> GetCourseEpisodes(int courseId);

        int AddEpisode(CourseEpisode episode, IFormFile episodeFile);

        bool AddEpisode(AddEpisodeViewModel episodeViewModel, string userName);

        bool DoesEpisodeExist(string fileName);

        bool UpdateEpisode(CourseEpisode episode, IFormFile episodeFile);

        bool DeleteEpisode(int episodeId);

        CourseEpisode GetEpisodeById(int episodeId);

        #endregion

        #region Comment

        CourseCommentsViewModel GetCourseComments(int courseId, int pageId = 1);
        bool AddComment(CourseComment comment);
        CourseComment GetCommentById(int commentId);

        #endregion

        #region Vote

        CourseVotesViewModel GetCourseVotes(int courseId);
        bool AddVote(string userName, int courseId, bool vote);

        #endregion
    }
}

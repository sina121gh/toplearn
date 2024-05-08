using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs.Question
{
    public class ShowCourseQuestionsViewModel
    {
        public IEnumerable<DataLayer.Entities.Questions.Question> Qeustions { get; set; }
        public int CourseId { get; set; }
        public int PageCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set;}
    }
}

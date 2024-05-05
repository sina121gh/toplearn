using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Questions;

namespace TopLearn.Core.DTOs.Question
{
    public class ShowSingleQuestionViewModel
    {
        public DataLayer.Entities.Questions.Question Question { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
